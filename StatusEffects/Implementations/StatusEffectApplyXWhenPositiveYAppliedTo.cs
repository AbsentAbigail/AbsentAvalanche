using System.Linq;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectApplyXWhenPositiveYAppliedTo : StatusEffectApplyXWhenYAppliedTo
{
    public bool negative;

    public override bool RunApplyStatusEvent(StatusEffectApply apply)
    {
        if (applyConstraints.Any(constraint => !constraint.Check(apply.target)))
            return false;

        return negative == apply.effectData.IsNegativeStatusEffect() && base.RunApplyStatusEvent(apply);
    }

    public override bool RunPostApplyStatusEvent(StatusEffectApply apply)
    {
        if (applyConstraints.Any(constraint => !constraint.Check(apply.target)))
            return false;

        return negative == apply.effectData.IsNegativeStatusEffect() && base.RunPostApplyStatusEvent(apply);
    }
}