#region

using System;
using System.Collections;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

internal class StatusInstantIncreaseCounter : StatusEffectInstant
{
    public override IEnumerator Process()
    {
        target.counter.current = Math.Min(target.counter.current + count, target.counter.max);
        yield return base.Process();
    }
}