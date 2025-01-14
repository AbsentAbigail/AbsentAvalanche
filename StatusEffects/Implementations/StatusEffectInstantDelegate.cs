using System;
using System.Collections;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectInstantDelegate : StatusEffectInstant
{
    public Action<StatusEffectInstant> Action;
    
    public override IEnumerator Process()
    {
        var code = AbsentUtils.GetStatusOf<StatusEffectInstantDelegate>(name).Action;
        (code ?? delegate { }).Invoke(this);
        yield return base.Process();
    }
}