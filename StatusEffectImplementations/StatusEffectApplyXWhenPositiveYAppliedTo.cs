#region

using System.Collections;
using System.Linq;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectApplyXWhenPositiveYAppliedTo : StatusEffectApplyXWhenYAppliedTo
{
    public bool negative;

    public override void Init()
    {
        PostApplyStatus += Run;
    }

    private new IEnumerator Run(StatusEffectApply apply)
    {
        return Run(GetTargets(new Hit(apply.applier, apply.target)), apply.count);
    }

    public override bool RunApplyStatusEvent(StatusEffectApply apply)
    {
        if (apply.effectData is null)
            return false;

        if (applyConstraints.Any(constraint => !constraint.Check(apply.target)))
            return false;

        return negative == apply.effectData.IsNegativeStatusEffect() && base.RunApplyStatusEvent(apply);
    }

    public override bool RunPostApplyStatusEvent(StatusEffectApply apply)
    {
        if (apply.effectData is null)
            return false;

        if (applyConstraints.Any(constraint => !constraint.Check(apply.target)))
            return false;

        return negative == apply.effectData.IsNegativeStatusEffect() && base.RunPostApplyStatusEvent(apply);
    }
}