using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class OnCardPlayedBoostSelf() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "Increase by 1", effectToApply: "Increase Effects")
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}