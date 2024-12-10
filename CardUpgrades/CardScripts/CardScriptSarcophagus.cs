using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using UnityEngine;

namespace AbsentAvalanche.CardUpgrades.CardScripts;

public class CardScriptSarcophagus : CardScript
{
    public CardData vessel;

    public override void Run(CardData target)
    {
        if (vessel is null)
            throw new ArgumentException("Vessel not given!");

        var inventory = References.PlayerData.inventory;
        var sarcophagus = vessel.Clone();


        var summon = AbsentUtils.GetStatusOf<StatusEffectSummon>(SummonSarcophagus.Name).InstantiateKeepName();
        summon.summonCard = target.Clone();

        var instantSummon = AbsentUtils.GetStatusOf<StatusEffectInstantSummon>(InstantSummonSarcophagus.Name)
            .InstantiateKeepName();
        instantSummon.targetSummon = summon;

        var applyX = AbsentUtils.GetStatusOf<StatusEffectApplyX>(WhenDestroyedSummonSarcophagus.Name)
            .InstantiateKeepName();
        applyX.effectToApply = instantSummon;
        applyX.textInsert = $"<{target.title}>";

        sarcophagus.startWithEffects =
        [
            AbsentUtils.SStack(Ethereal.Name, 3),
            new CardData.StatusEffectStacks(applyX, 2)
        ];

        object[] customData =
        [
            target.name,
            new SaveCollection<(string, int)>(target.attackEffects
                .Select<CardData.StatusEffectStacks,
                    (string, int)>(a => (a.data.name, a.count)).ToArray()),
            new SaveCollection<(string, int)>(target.startWithEffects.Select<CardData.StatusEffectStacks,
                (string, int)>(a => (a.data.name, a.count)).ToArray()),
            new SaveCollection<(string, int)>(target.traits.Select<CardData.TraitStacks,
                (string, int)>(a => (a.data.name, a.count)).ToArray()),
            new SaveCollection<string>(target.upgrades.Select(a => a.name).ToArray())
        ];

        sarcophagus.customData ??= new Dictionary<string, object>();
        var saveCollection = new SaveCollection<object>(customData);
        sarcophagus.customData.Add("Sarcophagus", saveCollection);

        inventory.deck.Add(sarcophagus);

        CardScriptDestroyCard.RemoveFromDeck(target);
        CardScriptDestroyCard.DestroyEntities(target);

        var card = CardManager.Get(sarcophagus, FindObjectOfType<CardControllerDeck>(), References.Player, inPlay: false, isPlayerCard: true);

        References.instance.StartCoroutine(Animation(card));
    }
    
    // Copied from Pokefrosts CardScriptCopy
    private static IEnumerator Animation(Card card)
    {
        yield return new WaitForSeconds(1f);

        yield return card.UpdateData();

        var deckDisplaySequence = FindObjectOfType<DeckDisplaySequence>();
        if (deckDisplaySequence is null)
            yield break;
        
        var entity = card.entity;
        deckDisplaySequence.activeCardsGroup.GetGrid(card).Add(entity);
        deckDisplaySequence.UpdatePositions();
    }
}