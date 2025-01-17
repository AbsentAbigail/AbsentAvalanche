using System.Collections;
using System.Linq;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectWhenXAppliedToRedirect : StatusEffectApplyX
{
    public ApplyToFlags whenAppliedFlags;
    public bool negativeStatus = true;
    public bool positiveStatus = true;
    public string[] whenAppliedTypes = [];

    public override bool RunApplyStatusEvent(StatusEffectApply apply)
    {
        if (!apply.applier || apply.applier == target || !apply.target || !apply.effectData ||
            apply.effectData.type.IsNullOrWhitespace() || target.silenced)
            return false;

        if (!CheckType(apply.effectData))
            return false;

        if (!CheckTarget(apply.target))
            return false;

        ActionQueue.Stack(new ActionSequence(Run(apply.effectData, apply.count)));
        apply.effectData = null;
        return false;
    }

    private IEnumerator Run(StatusEffectData effect, int amount)
    {
        effectToApply = effect;
        yield return Run(GetTargets(), amount);
    }

    private bool CheckTarget(Entity entity)
    {
        if (entity.statusEffects.Any(s => s.name == name))
            return false;

        var apply = applyToFlags;
        applyToFlags = whenAppliedFlags;
        var entities = GetTargets();
        applyToFlags = apply;
        return entities.Contains(entity);
    }

    private bool CheckType(StatusEffectData effectData)
    {
        if (!effectData.isStatus)
            return false;

        if (!(negativeStatus == effectData.IsNegativeStatusEffect() ||
              positiveStatus != effectData.IsNegativeStatusEffect()))
            return false;

        return whenAppliedTypes.Length == 0 || whenAppliedTypes.Contains(effectData.type);
    }
}