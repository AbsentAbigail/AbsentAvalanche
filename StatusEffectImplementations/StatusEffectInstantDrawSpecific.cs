using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffectImplementations.Actions;
using Dead;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectInstantDrawSpecific : StatusEffectInstant
{
    public TargetConstraint[] constraints;
    public bool all;
    public StatusEffectData applyEffect;
    
    public override IEnumerator Process()
    {
        var drawPile = References.Player.drawContainer;
        var matchingCards = drawPile.Where(card => constraints.All(constraint => constraint.Check(card))).ToArray();

        if (!matchingCards.Any() && NoTargetTextSystem.Exists())
        {
            yield return NoTargetTextSystem.Run(target, NoTargetType.NoCardsToDraw);
            yield return Remove();
            yield break;
        }
        
        if (all)
        {
            matchingCards.Do(Draw);
        }
        else
        {
            matchingCards.InRandomOrder().Take(GetAmount()).Do(Draw);
        }
        
        yield return Remove();
    }

    private void Draw(Entity entity)
    {
        ActionQueue.Stack(new ActionMove(entity, References.Player.handContainer));
        entity.flipper.FlipUp();
        
        if (!applyEffect)
        {
            return;
        }
        
        ActionQueue.Stack(new ActionApplyStatus(entity, applier, applyEffect, GetAmount()));
        ActionQueue.Stack(new ActionUpdateText(entity));
    }
}

// For Throes of Chaos. Draw a card (random or top, from deck, discard, or both, using a predicate to choose) to hand, optionally apply some effects to it.
public class StatusEffectDrawRandomCardWithPredicate : StatusEffectInstant
{
    public Predicate<CardData> predicate;
    public CardData.StatusEffectStacks[] addEffectStacks;
    public bool equalToCount;
    public int drawNumber;  // 0 is default - draw equal to the stack
    public bool random;
    public bool deck = true;
    public bool discard;
    
    public override IEnumerator Process()
    {
        var player = References.Player;

        List<Entity> cards = [];
        if (deck)
            cards = cards.Concat(player.drawContainer.entities).ToList();
        if (discard)
            cards = cards.Concat(player.discardContainer.entities).ToList();

        var predicate1 = Absent.GetStatusOf<StatusEffectDrawRandomCardWithPredicate>(name).predicate;
        if (predicate1 is null)
            throw new ArgumentException("No predicate found");

        var targets = cards.Count(card => predicate1.Invoke(card.data));

        if (targets == 0)
        {
            LogHelper.Log(" No tutor targets :(");
            if (NoTargetTextSystem.Exists())
            {
                yield return NoTargetTextSystem.Run(target, NoTargetType.NoCardsToDraw);
            }
            yield return Remove();
            yield break;
        }

        if (equalToCount)
        {
            LogHelper.Log(" I have a count of " + count);
            foreach (var stacks in addEffectStacks)
            {
                LogHelper.Log(" " + stacks.data.name + " has a count of " + stacks.count);
                stacks.count = count;
                LogHelper.Log(" " + stacks.data.name + " has a new count of " + stacks.count);
            }
        }
        if (drawNumber > 0)
        {
            ActionQueue.Stack(new ActionDrawPredicate(player, predicate1, addEffectStacks, drawNumber, random: random, deck: deck, discard: discard), fixedPosition: true);
        }
        else
        {
            ActionQueue.Stack(new ActionDrawPredicate(player, predicate1, addEffectStacks, targets, random: random, deck: deck, discard: discard), fixedPosition: true);
        }

        yield return base.Process();
    }
}

// Needed for Throes of Chaos to work. Checks a predicate against a card. Needs an effect array to apply, but you can just use []
public class ActionDrawPredicate : ActionDraw
{
    public readonly new Character character;
    public Predicate<CardData> predicate;
    public CardData.StatusEffectStacks[] addEffectStacks;
    public bool random;
    public bool deck;
    public bool discard;
    
    public ActionDrawPredicate(Character character, Predicate<CardData> predicate, CardData.StatusEffectStacks[] addEffectStacks, int count = 1, float pauseBetween = 0.1F, bool deck = true, bool discard = false, bool random = false) : base(character, count, pauseBetween)
    {
        this.character = character;
        this.count = count;
        this.pauseBetween = pauseBetween;
        this.predicate = predicate;
        this.random = random;
        this.deck = deck;
        this.discard = discard;
        this.addEffectStacks = addEffectStacks;
    }
    
    public override IEnumerator Run()
    {
        if (count <= 0 || !character.drawContainer || !character.handContainer || !character.discardContainer)
        {
            yield break;
        }

        Events.InvokeCardDraw(count);
        while (count > 0)
        {
            yield return Sequences.Wait(pauseBetween);  // Wait

            List<Entity> cards = [];
            if (deck)
            {
                cards = cards.Concat(character.drawContainer.entities).ToList();
            }
            if (discard)
            {
                cards = cards.Concat(character.discardContainer.entities).ToList();
            }
            if (random)
            {
                cards = InPettyRandomOrder(cards).ToList();
            }

            Entity foundCard = null;
            for (var i = cards.Count - 1; i >= 0; i--)
            {
                if (predicate.Invoke(cards[i].data))
                {
                    foundCard = cards[i];  // Get the top card of the potentially shuffled pile
                    break;
                }
            }

            // if (!foundCard)  // If that didn't work, shuffle the discard to try and get the top card again
            // {
            //     Events.InvokeCardDrawEnd();
            //     LogHelper.Log(" No tutor targets :(");
            // }

            if (foundCard)  // Move the new card to hand
            {
                LogHelper.Log(" Card found is " + foundCard.name + ". Top card in deck is " + character.drawContainer.GetTop().name);
                yield return Sequences.CardMove(foundCard, [character.handContainer]);
                character.handContainer.TweenChildPositions();

                foreach (var stack in addEffectStacks)
                {
                    ActionQueue.Stack(new ActionApplyStatus(foundCard, null, stack.data, stack.count));
                }

                foundCard.display.promptUpdateDescription = true;
                foundCard.PromptUpdate();

                ActionQueue.Stack(new ActionSequence(foundCard.UpdateTraits()) { note = $"[{foundCard}] Update Traits" });
            }

            count--;
        }

        Events.InvokeCardDrawEnd();
        ActionQueue.Stack(new ActionRevealAll(character.handContainer));
    }
    
    private static IOrderedEnumerable<T> InPettyRandomOrder<T>(IEnumerable<T> source)
    {
        return source.OrderBy(_ => PettyRandom.Range(0f, 1f));
    }
}
