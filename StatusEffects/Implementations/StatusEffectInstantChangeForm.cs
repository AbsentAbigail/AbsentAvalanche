using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AbsentUtilities;
using DeadExtensions;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Serialization;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectInstantChangeForm : StatusEffectInstant
{
    public CardData[] phaseOptions;
    public int splitCount;
    public CardAnimation animation;
    public bool keepCardType;
    public bool keepUpgrades = true;
    public CardData.StatusEffectStacks bossTransform;
    public int healthChange;
    public int damageChange;
    public int counterChange;
    public CardData.StatusEffectStacks[] startWithEffects;

    public override IEnumerator Process()
    {
        var slots = Battle.instance.GetSlots(target.owner).Count(s => s is null || s.entities.Count <= 0);
        if (slots < splitCount - 1)
        {
            yield return Text(NoTargetType.CantSplit);
            yield return Remove();
            yield break;
        }

        if (phaseOptions.Length <= 0)
            throw new ArgumentException("Next phase not given!");

        var cards = GetCards();
        
        cards.Do(c =>
        {
            if (c.hasHealth)
                c.hp += healthChange;
            if (c.hasAttack)
                c.damage += damageChange;
            if (c.counter > 0)
                c.counter += counterChange;
            c.startWithEffects =
            [
                ..c.startWithEffects,
                ..startWithEffects
            ];
        });
        
        var originalCardType = target.data.cardType;
        if (CheckCardType(originalCardType))
            cards.Do(c => c.cardType = originalCardType);
        
        if (bossTransform != null && bossTransform.data != null)
        {
            var randomCard = cards.RandomItem();
            randomCard.startWithEffects = [
                ..randomCard.startWithEffects,
                bossTransform
            ];
        }
        
        AddCharms(cards);
        var targetCopy = target.data.InstantiateKeepName();
        var baseCard = AbsentUtils.GetCard(target.name);
        foreach (var upgrade in targetCopy.upgrades.ToArray())
            upgrade.UnAssign(targetCopy);
        
        var healthDiff = targetCopy.hp - baseCard.hp;
        var damageDiff = targetCopy.damage - baseCard.damage;
        var counterDiff = targetCopy.counter - baseCard.counter;
        
        foreach (var card in cards)
        {
            if (card.hasHealth)
                card.hp = Math.Max(1, card.hp + healthDiff);
            if (card.hasAttack)
                card.damage += damageDiff;
            if (card.counter > 0)
                card.counter = Math.Max(1, card.counter + counterDiff);
        }
        
        var action = new ActionChangeForm(target, cards, animation)
        {
            priority = 10
        };
        ActionQueue.Stack(action, true);
        
        yield return Remove();
    }

    private void AddCharms(CardData[] cards)
    {
        if (!keepUpgrades)
            return;

        var upgrades = target.data.upgrades;

        cards.Do(card =>
        {
            card.charmSlots = target.data.charmSlots;
            foreach (var upgrade in upgrades.Where(upgrade => upgrade.CanAssign(card)))
                upgrade.Clone().Assign(card);
        });
    }

    private CardData[] GetCards()
    {
        var remaining = splitCount;
        List<CardData> cards = [];

        while (cards.Count < splitCount)
            cards.AddRange(
                remaining < phaseOptions.Length
                    ? phaseOptions
                    : phaseOptions.RandomItems(remaining)
            );

        return cards.Select(a => a.Clone()).ToArray();
    }

    private bool CheckCardType(CardType cardType)
    {
        return keepCardType
           || cardType.name == "Leader"
           || cardType.name == "Miniboss"
           || cardType.name == "Boss"
           || cardType.name == "BossSmall";
    }
    
    private IEnumerator Text(NoTargetType noTargetType)
    {
        if (!NoTargetTextSystem.Exists())
            yield break;

        yield return NoTargetTextSystem.Run(target, noTargetType);
    }

    private class ActionChangeForm(Entity entity, CardData[] newPhases, CardAnimation animation)
        : ActionChangePhase(entity, newPhases, animation)
    {
        public override IEnumerator Run()
        {
            if (!entity.IsAliveAndExists()) yield break;

            Events.InvokeEntityChangePhase(entity);
            var routine = new Routine(CreateNewCards());

            PauseMenu.Block();
            DeckpackBlocker.Block();
            if (Deckpack.IsOpen && References.Player.entity.display is CharacterDisplay display)
                display.CloseInventory();
            var animationSystem = FindObjectOfType<ChangePhaseAnimationSystem>();
            if (animationSystem)
                yield return animationSystem.Focus(entity);
            if (animation)
                yield return animation.Routine(entity);
            foreach (var action in ActionQueue.GetActions())
                switch (action)
                {
                    case ActionTrigger actionTrigger:
                        if (actionTrigger.entity == entity)
                            ActionQueue.Remove(action);
                        break;
                    case ActionEffectApply actionEffectApply:
                        actionEffectApply.TryRemoveEntity(entity);
                        break;
                }

            var splitAction = new ActionSequence(Split(entity))
            {
                note = "Split boss",
                priority = 10
            };
            ActionQueue.Stack(splitAction, true);

            if (!animationSystem) yield break;

            var animationAction = new ActionSequence(animationSystem.UnFocus())
            {
                note = "Unfocus boss",
                priority = 10
            };
            ActionQueue.Stack(animationAction, true);
        }


        private IEnumerator Split(Entity splittingEntity)
        {
            entity.alive = false;
            while (loadingNewCards)
                yield return null;
            var num = 0;
            var count = entity.actualContainers.Count;
            var toMove = new Dictionary<CardContainer, List<Entity>>();
            var freeSlots = Battle.instance.GetSlots(splittingEntity.owner)
                .Where(s => s is not null && s.entities.Count <= 0).ToArray();
            var entitySlots = entity.actualContainers.Count;
            foreach (var newCard in newCards)
            {
                var index = num % count;
                var actualContainer = num < entitySlots ? entity.actualContainers[num] : freeSlots[num - entitySlots];

                if (toMove.ContainsKey(actualContainer))
                {
                    var container = entity.containers[index];
                    if (toMove.ContainsKey(container))
                        toMove[container].Add(newCard);
                    else
                        toMove[container] = [newCard];
                }
                else
                {
                    toMove[actualContainer] = [newCard];
                }

                ++num;
            }

            var position = entity.transform.position;
            entity.RemoveFromContainers();
            CardManager.ReturnToPool(entity);
            foreach (var keyValuePair in toMove)
            {
                var cardContainer = keyValuePair.Key;
                var entityList = keyValuePair.Value;

                if (entityList == null) continue;

                foreach (var entity1 in entityList)
                {
                    cardContainer.Add(entity1);
                    var transform = entity1.transform;
                    transform.localScale = entity1.GetContainerScale();
                    var containerWorldPosition = entity1.GetContainerWorldPosition();
                    transform.position = Vector3.Lerp(position, containerWorldPosition, 0.1f);
                    LeanTween.move(entity1.gameObject, containerWorldPosition, PettyRandom.Range(0.8f, 1.2f))
                        .setEaseOutElastic();
                    entity1.wobbler.WobbleRandom();
                }
            }

            var objectOfType1 = FindObjectOfType<ChangePhaseAnimationSystem>();
            var objectOfType2 = FindObjectOfType<MinibossIntroSystem>();
            foreach (var newCard in newCards)
            {
                if (objectOfType1)
                {
                    objectOfType1.RemoveTarget(splittingEntity);
                    objectOfType1.Assign(newCard);
                }

                if (objectOfType2)
                    objectOfType2.Ignore(newCard);
            }

            var action = new ActionSequence(FinalSplit(toMove))
            {
                note = "Final boss split",
                priority = 10
            };
            ActionQueue.Stack(action, true);
        }
    }
}