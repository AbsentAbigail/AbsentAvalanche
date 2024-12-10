using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class OnCardPlayedTriggerAllyAhead() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "Trigger ally in front", effectToApply: "Trigger",
    applyToFlags: StatusEffectApplyX.ApplyToFlags.AllyInFrontOf)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}