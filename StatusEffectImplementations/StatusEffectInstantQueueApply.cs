#region

using System.Collections;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

internal class StatusEffectInstantQueueApply : StatusEffectInstant
{
    public StatusEffectData effectToApply;

    public override IEnumerator Process()
    {
        yield return ActionQueue.Stack(new ActionApplyStatus(target, applier, effectToApply, GetAmount()));

        yield return base.Process();
    }
}