#region

using System.Collections;
using UnityEngine;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectSafeTemporaryTrait : StatusEffectTemporaryTrait
{
    private int _finished;
    private int _queued;

    public override IEnumerator StackRoutine(int stacks)
    {
        var current = _queued;
        _queued++;
        yield return new WaitUntil(() => _finished == current);
        yield return base.StackRoutine(stacks);
        _finished++;
    }
}