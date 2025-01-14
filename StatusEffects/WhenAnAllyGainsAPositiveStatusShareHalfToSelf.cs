using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhenAnAllyGainsAPositiveStatusShareHalfToSelf() : AbstractApplyXStatus<StatusEffectShareStatus>(
    Name,
    subscribe: status =>
    {
        status.whenAppliedFlags = StatusEffectApplyX.ApplyToFlags.Allies;
        status.negativeStatus = false;
        status.equalAmountBonusMult = -0.49f;
        status.applyEqualAmount = true;
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}