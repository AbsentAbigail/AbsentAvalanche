using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dead;
using UnityEngine;

namespace AbsentAvalanche.StatusEffects.Implementations;

internal class StatusEffectInstantCombineCard : StatusEffectInstant
{
    public string combineSceneName = "CardCombine";

    public string[] cardNames = [];
    public string resultingCardName = "";

    public bool checkHand = true;
    public bool checkDeck = true;
    public bool checkBoard = true;

    public bool keepUpgrades = true;
    public List<CardUpgradeData> extraUpgrades = [];

    public bool spawnOnBoard = false;
    public bool changeDeck = false;

    public override IEnumerator Process()
    {
        var combo = new Combo
        {
            CardNames = cardNames,
            ResultingCardName = resultingCardName
        };

        List<Entity> fullDeck = [];
        if (checkHand)
            fullDeck.AddRange(References.Player.handContainer.ToList());

        if (checkDeck)
        {
            fullDeck.AddRange(References.Player.drawContainer.ToList());
            fullDeck.AddRange(References.Player.discardContainer.ToList());
        }

        if (checkBoard)
            fullDeck.AddRange(Battle.GetCardsOnBoard(References.Player).ToList());


        if (combo.AllCardsInDeck(fullDeck))
        {
            var action = new CombineAction(keepUpgrades, extraUpgrades, spawnOnBoard, target.containers[0])
            {
                CombineSceneName = combineSceneName,
                TooFuse = combo.FindCards(fullDeck),
                Combo = combo
            };

            if (changeDeck) EditDeck(combo.CardNames, combo.ResultingCardName);

            var queueAction = ActionQueue.instance.queue.All(playAction => playAction.GetType() != action.GetType());

            if (queueAction)
                ActionQueue.Stack(action);
        }

        yield return base.Process();
    }

    public void EditDeck(string[] cardsToRemove, string cardToAdd)
    {
        List<CardData> oldCards = [];

        foreach (var cardToRemove in cardsToRemove)
        foreach (var card in References.Player.data.inventory.deck)
        {
            if (card.name != cardToRemove || oldCards.Contains(card))
                continue;
            oldCards.Add(card);
            break;
        }

        if (oldCards.Count != cardsToRemove.Length)
            return;

        List<CardUpgradeData> upgrades = [];

        foreach (var card in oldCards)
        {
            if (keepUpgrades) upgrades.AddRange(card.upgrades.Select(u => u.Clone()));

            References.Player.data.inventory.deck.Remove(card);
        }

        var cardDataClone = AddressableLoader.GetCardDataClone(cardToAdd);

        upgrades.AddRange(extraUpgrades.Select(u => u.Clone()));

        foreach (var upgrade in upgrades) upgrade.Assign(cardDataClone);

        References.Player.data.inventory.deck.Add(cardDataClone);
    }

    private struct Combo
    {
        public string[] CardNames;
        public string ResultingCardName;

        public bool AllCardsInDeck(List<Entity> deck)
        {
            return CardNames.All(cardName => HasCard(cardName, deck));
        }

        public List<Entity> FindCards(List<Entity> deck)
        {
            List<Entity> tooFuse = [];
            var array = CardNames;
            foreach (var cardName in array)
            foreach (var item in deck.Where(item => item.data.name == cardName))
            {
                tooFuse.Add(item);
                break;
            }

            return tooFuse;
        }

        private static bool HasCard(string cardName, List<Entity> deck)
        {
            return deck.Any(item => item.data.name == cardName);
        }
    }

    private class CombineAction(
        bool keepUpgrades,
        List<CardUpgradeData> extraUpgrades,
        bool spawnOnBoard,
        CardContainer row)
        : PlayAction
    {
        public string CombineSceneName;

        public Combo Combo;

        public List<Entity> TooFuse;

        public override IEnumerator Run()
        {
            return CombineSequence(Combo, TooFuse);
        }

        private IEnumerator CombineSequence(Combo combo, List<Entity> tooFuse)
        {
            CombineCardSequence combineSequence = null;
            yield return SceneManager.Load(
                CombineSceneName,
                SceneType.Temporary,
                scene => combineSequence = scene.FindObjectOfType<CombineCardSequence>()
            );
            if (combineSequence)
                yield return combineSequence.Run2(
                    tooFuse,
                    combo.ResultingCardName,
                    keepUpgrades,
                    extraUpgrades,
                    spawnOnBoard,
                    row
                );

            yield return SceneManager.Unload(CombineSceneName);
        }
    }
}

public static class CombineCardSequenceExtension
{
    public static IEnumerator Run2(this CombineCardSequence seq, List<Entity> cardsToCombine, string resultingCard,
        bool keepUpgrades, List<CardUpgradeData> extraUpgrades, bool spawnOnBoard, CardContainer row)
    {
        var cardDataClone = AddressableLoader.GetCardDataClone(resultingCard);

        List<CardUpgradeData> upgrades = [];
        if (keepUpgrades)
            foreach (var ent in cardsToCombine)
                upgrades.AddRange(ent.data.upgrades.Select(u => u.Clone()));

        upgrades.AddRange(extraUpgrades.Select(u => u.Clone()));

        foreach (var upgrade in upgrades)
            upgrade.Assign(cardDataClone);

        yield return Run2(seq, cardsToCombine.ToArray(), cardDataClone, spawnOnBoard, row);
    }

    private static IEnumerator Run2(
        this CombineCardSequence seq, Entity[] entities, CardData finalCard, bool spawnOnBoard, CardContainer row
    )
    {
        PauseMenu.Block();
        var card = CardManager.Get(
            finalCard,
            Battle.instance.playerCardController,
            References.Player,
            false,
            true
        );
        card.transform.localScale = Vector3.one * 1f;
        card.transform.SetParent(seq.finalEntityParent);
        var finalEntity = card.entity;
        var clump = new Routine.Clump();
        var array = entities;
        foreach (var entity in array)
            clump.Add(entity.display.UpdateData());

        clump.Add(finalEntity.display.UpdateData());
        clump.Add(Sequences.Wait(0.5f));
        yield return clump.WaitForEnd();

        array = entities;
        foreach (var entity2 in array)
            entity2.RemoveFromContainers();

        array = entities;
        foreach (var entity in array)
            entity.transform.localScale = Vector3.one * 0.8f;

        seq.fader.In();
        var zero = Vector3.zero;
        array = entities;
        zero = array.Aggregate(zero, (current, entity3) => current + entity3.transform.position);

        zero /= entities.Length;

        seq.group.position = zero;
        array = entities;
        foreach (var entity4 in array)
        {
            var transform = Object.Instantiate(seq.pointPrefab, entity4.transform.position, Quaternion.identity,
                seq.group);
            transform.gameObject.SetActive(true);
            entity4.transform.SetParent(transform);
            entity4.flipper.FlipUp();
            seq.points.Add(transform);
            LeanTween.alphaCanvas(((Card)entity4.display).canvasGroup, 1f, 0.4f).setEaseInQuad();
        }

        foreach (var point in seq.points)
            LeanTween.moveLocal(to: point.localPosition.normalized, gameObject: point.gameObject, time: 0.4f)
                .setEaseInQuart();

        yield return new WaitForSeconds(0.4f);

        Events.InvokeScreenShake();
        array = entities;
        foreach (var entity in array)
            entity.wobbler.WobbleRandom();

        foreach (var point2 in seq.points)
            LeanTween.moveLocal(to: point2.localPosition.normalized * 3f, gameObject: point2.gameObject, time: 1f)
                .setEase(seq.bounceCurve);

        LeanTween.moveLocal(seq.group.gameObject, new Vector3(0f, 0f, -2f), 1f).setEaseInOutQuad();
        LeanTween.rotateZ(seq.group.gameObject, PettyRandom.Range(160f, 180f), 1f).setOnUpdateVector3(_ =>
        {
            foreach (var point3 in seq.points)
                point3.transform.eulerAngles = Vector3.zero;
        }).setEaseInOutQuad();
        yield return new WaitForSeconds(1f);

        Events.InvokeScreenShake();
        if (seq.ps)
            seq.ps.Play();

        seq.combinedFx.SetActive(true);

        finalEntity.transform.position = Vector3.zero;
        array = entities;
        foreach (var entity in array)
            CardManager.ReturnToPool(entity);

        seq.group.transform.localRotation = Quaternion.identity;
        finalEntity.curveAnimator.Ping();
        finalEntity.wobbler.WobbleRandom();

        yield return new WaitForSeconds(1f);

        seq.fader.gameObject.Destroy();
        PauseMenu.Unblock();

        var flag = true;
        if (spawnOnBoard)
        {
            if (row.owner == References.Player && row.Count != 3)
            {
                yield return Sequences.CardMove(finalEntity, [row]);
                finalEntity.inPlay = true;
                flag = false;
            }

            if (flag)
                for (var i = 0; i < 2; i++)
                {
                    row = Battle.instance.GetRow(References.Player, i);
                    if (row.Count == 3)
                        continue;
                    yield return Sequences.CardMove(finalEntity, [row]);
                    finalEntity.inPlay = true;
                    flag = false;

                    break;
                }

            if (finalEntity.inPlay)
                foreach (var statusEffect in finalEntity.statusEffects
                             .Where(s => s is StatusEffectWhileActiveX).ToArray())
                    if (((StatusEffectWhileActiveX)statusEffect).CanActivate())
                        yield return ((StatusEffectWhileActiveX)statusEffect).Activate();
        }

        if (flag)
        {
            yield return Sequences.CardMove(finalEntity, [References.Player.handContainer]);
            finalEntity.inPlay = true;
        }

        References.Player.handContainer.TweenChildPositions();
        ActionQueue.Stack(new ActionReveal(finalEntity));
        Events.InvokeEntityShowUnlocked(finalEntity);
    }
}