﻿using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

internal class Blackfisk() : AbstractCompanion(
    Name, "Bläckfisk",
    8, 0, 8,
    subscribe: card =>
    {
        card.attackEffects = [AbsentUtils.SStack(InstantIncreaseCurrentCounter.Name)];
        card.startWithEffects = [AbsentUtils.SStack("MultiHit", 7)];
        card.traits =
        [
            AbsentUtils.TStack("Pull"),
            AbsentUtils.TStack("Aimless")
        ];
        card.greetMessages =
        [
            "The octopus is a truly unique marine animal with its 8 arms and the ability to camouflage itself. Imagine all the exciting adventures your child can experience with such a companion by their side.",
            "8 arms to give 8 times better hugs!"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "8 arms to give 8 times better hugs!";
    protected override string IdleAnimation => "PulseAnimationProfile";
    protected override string BloodProfile => "Blood Profile Blue (x2)";
}