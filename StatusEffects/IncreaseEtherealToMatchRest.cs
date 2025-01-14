using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class IncreaseEtherealToMatchRest() : AbstractApplyXStatus<StatusEffectRest>(
    Name,
    canBoost: true,
    effectToApply: Ethereal.Name,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.Self,
    subscribe: status => { status.applyEqualAmount = true; }
)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}