using System.Collections;

namespace AbsentAvalanche.StatusEffects.Implementations;

internal class StatusOnHitEat : StatusEffectApplyX
{
    public override void Init()
    {
        OnHit += Check;
    }

    private IEnumerator Check(Hit hit)
    {
        yield return StatusEffectSystem.Apply(hit.target, target, effectToApply, 1);
        hit.countsAsHit = false;
    }

    public override bool RunHitEvent(Hit hit)
    {
        if (hit.attacker != target)
            return false;

        var result = base.RunHitEvent(hit);
        var shouldKill = hit.target.hp.current <= hit.damage;
        return result && shouldKill;
    }
}