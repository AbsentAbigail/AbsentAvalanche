using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhenAllyHitIncreaseEffects() : AbstractApplyXStatus<StatusEffectApplyXWhenAllyIsHit>(
    Name, "Boost own effects by {a} when an ally is hit",
    effectToApply: "Increase Effects"
    )
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}