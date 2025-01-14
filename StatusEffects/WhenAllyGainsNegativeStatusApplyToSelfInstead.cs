using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhenAllyGainsNegativeStatusApplyToSelfInstead() : AbstractApplyXStatus<StatusEffectWhenXAppliedToRedirect>(
    Name,
    subscribe: status =>
    {
        status.positiveStatus = false;
        status.whenAppliedFlags = StatusEffectApplyX.ApplyToFlags.Allies;
        status.applyEqualAmount = true;
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}