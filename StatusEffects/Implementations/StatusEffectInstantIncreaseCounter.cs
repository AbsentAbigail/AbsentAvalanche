using System;
using System.Collections;

namespace AbsentAvalanche.StatusEffects.Implementations;

internal class StatusInstantIncreaseCounter : StatusEffectInstant
{
    public override IEnumerator Process()
    {
        target.counter.current = Math.Min(target.counter.current + count, target.counter.max);
        yield return base.Process();
    }
}