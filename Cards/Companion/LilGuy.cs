﻿using AbsentAvalanche.StatusEffects;
using AbsentAvalanche.Traits;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Companion;

public class LilGuy() : AbstractCompanion(Name, "Lil' Guy", 4, 2, 4,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(FakeCalm.Name, 3)
        ];
        card.traits =
        [
            AbsentUtils.TStack(Scavenge.Name)
        ];
        card.createScripts =
        [
            .. card.createScripts,
            ScriptableHelper.CreateScriptable<CardScriptGiveUpgrade>(
                "Add Chuckle Charm",
                script => script.upgradeData = AbsentUtils.GetCardUpgrade("CardUpgradeRemoveCharmLimit")
            )
        ];
        card.charmSlots = int.MaxValue - 100_001;
    })
{
    public const string Name = "LilGuy";
    public override string FlavourText => "Adventure time!";
}