using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class OnCardPlayedCleanseSelf() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "<keyword=cleanse> self",
    effectToApply: "Cleanse")
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}