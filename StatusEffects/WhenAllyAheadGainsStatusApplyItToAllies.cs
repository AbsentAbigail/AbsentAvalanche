using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhenAllyAheadGainsStatusApplyItToAllies() : AbstractApplyXStatus<StatusEffectShareStatus>(
    Name, "When ally in front gains a positive status, share it to all other allies",
    effectToApply: null,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.Allies,
    subscribe: status =>
    {
        status.whenAppliedFlags = StatusEffectApplyX.ApplyToFlags.AllyInFrontOf;
        status.negativeStatus = false;
        status.applyEqualAmount = true;
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}