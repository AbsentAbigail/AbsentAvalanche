using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

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
    public const string Name = "When Ally Ahead Gains Status Apply It To Allies";
}