﻿using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Cards.Companion;

public class May() : AbstractCompanion(
    Name, "May",
    4, null, 3,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack("Teeth", 2),
            AbsentUtils.SStack("On Turn Apply Teeth To Self"),
            AbsentUtils.SStack(OnKillIncreaseHealthPermanent.Name)
        ];
    })
{
    public const string Name = "May";
    public override string FlavourText => "Very cuddly when she's not busy ruining a toy city";
    protected override string IdleAnimation => "GiantAnimationProfile";
    protected override string BloodProfile => "Blood Profile Pink Wisp";
}