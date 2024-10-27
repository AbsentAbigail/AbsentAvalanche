using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class IncreaseEtherealToMatchRest() : AbstractApplyXStatus<StatusEffectRest>(
    Name,
    canBoost: true,
    effectToApply: Ethereal.Name,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.Self,
    subscribe: status => { status.applyEqualAmount = true; }
)
{
    public const string Name = "Increase Ethereal To Match Rest";
}