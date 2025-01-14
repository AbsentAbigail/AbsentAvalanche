using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class OnHitGainEqualBling() : AbstractApplyXStatus<StatusEffectApplyXOnHit>(
    Name, "Gain <keyword=blings> equal to damage dealt",
    effectToApply: "Gain Gold",
    subscribe: status => { status.applyEqualAmount = true; })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}