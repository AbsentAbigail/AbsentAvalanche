﻿using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

internal class Kramig() : AbstractCompanion(
    Name, "Kramig",
    8, 3, 4,
    subscribe: card =>
    {
        card.startWithEffects = [AbsentUtils.SStack(DealAdditionalDamageForEachDamagedAlly.Name, 2)];
        card.greetMessages =
        [
            "In the wild, an adult panda eats about 83 pounds of bamboo – every day! But this black and white softie doesn’t need any food, just a lot of love.",
            "Protects its friends"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Protects its friends";
}