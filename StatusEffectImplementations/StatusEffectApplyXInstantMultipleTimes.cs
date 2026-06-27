using System.Collections;
using UnityEngine;

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectApplyXInstantMultipleTimes : StatusEffectApplyXInstant
{
    public int times = 3;
    public float delay = 0.2f;
    
    public override void Init()
    {
        OnBegin += Process;
    }

    private new IEnumerator Process()
    {
        for (var i = 0; i < times; i++)
        {
            yield return Run(GetTargets());
            yield return new WaitForSeconds(delay);
        }
        yield return Remove();
    }
}