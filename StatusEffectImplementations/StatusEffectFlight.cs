using System;
using System.Collections;

namespace AbsentAvalanche.StatusEffectImplementations;

internal class StatusEffectFlight : StatusEffectData
{
    public override void Init()
    {
        OnHit += Check;
    }

    public override bool RunHitEvent(Hit hit)
    {
        return hit.target == target && hit.damage > 0;
    }

    public IEnumerator Check(Hit hit)
    {
        var halfDamage = hit.damage / 2f;
        hit.damage = (int)Math.Ceiling(halfDamage);
        hit.damageBlocked = (int)halfDamage;

        if (!hit.canRetaliate || !hit.Offensive || !hit.BasicHit)
        {
            yield break;
        }
        
        var amount = 1;
        Events.InvokeStatusEffectCountDown(this, ref amount);
        yield return CountDown(target, amount);
    }
}