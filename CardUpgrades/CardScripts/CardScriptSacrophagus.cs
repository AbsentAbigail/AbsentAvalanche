using System;
using System.Collections.Generic;
using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

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
            AbsentUtils.SStack(Ethereal.Name, 4),
            new CardData.StatusEffectStacks(applyX, 2)
        ];
        
        sarcophagus.customData ??= new Dictionary<string, object>();
        sarcophagus.customData.Add("Sarcophagus", target.Clone());
        
        inventory.deck.Add(sarcophagus);

        CardScriptDestroyCard.RemoveFromDeck(target);
        CardScriptDestroyCard.DestroyEntities(target);
    }
}