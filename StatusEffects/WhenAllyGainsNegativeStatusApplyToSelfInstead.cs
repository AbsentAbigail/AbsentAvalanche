using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

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
    public const string Name = "WhenAllyGainsNegativeStatusApplyToSelfInstead";
}