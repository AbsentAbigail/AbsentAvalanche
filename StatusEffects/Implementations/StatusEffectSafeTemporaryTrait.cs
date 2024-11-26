using System.Collections;
using UnityEngine;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectSafeTemporaryTrait : StatusEffectTemporaryTrait
{
    private int _queued;
    private int _finished;

    public override IEnumerator StackRoutine(int stacks)
    {
        var current = _queued;
        _queued++;
        yield return new WaitUntil(() => _finished == current);
        yield return base.StackRoutine(stacks);
        _finished++;
    }
}