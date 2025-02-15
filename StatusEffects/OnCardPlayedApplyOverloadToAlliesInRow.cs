﻿using AbsentUtilities;
using HarmonyLib;
using static StatusEffectApplyX;

namespace AbsentAvalanche.StatusEffects;

internal class OnCardPlayedApplyOverloadToAlliesInRow() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "Apply <{a}><keyword=overload> to allies in row",
    true, true,
    "Overload",
    ApplyToFlags.AlliesInRow)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}