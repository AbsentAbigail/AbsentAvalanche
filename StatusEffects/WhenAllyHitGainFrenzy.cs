using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhenAllyHitGainFrenzy() : AbstractApplyXStatus<StatusEffectApplyXWhenAllyIsHit>(
    Name, "When an ally is hit, gain {0}",
    canBoost: true,
    effectToApply: "MultiHit",
    subscribe: status => status.textInsert = "<x{a}><keyword=frenzy>"
    )
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}