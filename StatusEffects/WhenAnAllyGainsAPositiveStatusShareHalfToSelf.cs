using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

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
    public const string Name = "WhenAnAllyGainsAPositiveStatusShareHalfToSelf";
}