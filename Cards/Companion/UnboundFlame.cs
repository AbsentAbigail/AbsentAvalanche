﻿using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Companion;

internal class UnboundFlame() : AbstractCompanion(
    Name, "Unbound Flame",
    5, 0, 3,
    Pools.None,
    card =>
    {
        card.attackEffects = [AbsentUtils.SStack("Overload", 3)];
        card.startWithEffects =
        [
            AbsentUtils.SStack(OnCardPlayedApplyOverloadToAlliesInRow.Name, 3)
        ];
        card.traits = [AbsentUtils.TStack("Barrage")];
    })
{
    public const string Name = "UnboundFlame";
    public override string FlavourText => "Ethereal flames fill the space around";
    protected override string BloodProfile => "Blood Profile Black";
    protected override string IdleAnimation => "FloatAnimationProfile";
}