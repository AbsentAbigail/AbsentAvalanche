#region

using System.Collections;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectApplyXWhenCertainUnitIsHit : StatusEffectApplyXWhenUnitIsHit
{
    public ApplyToFlags whenHitFlags = ApplyToFlags.AllyInFrontOf;

    public override void Init()
    {
        PostHit += Check;
    }

    public override bool RunPostHitEvent(Hit hit)
    {
        if (!CheckTarget(hit.target))
            return false;
        return target.enabled && target.alive && hit.canRetaliate && hit.BasicHit && hit.Offensive &&
               (!targetMustBeAlive || hit.target.alive) && Battle.IsOnBoard(target) && CheckConstraints(hit.target) &&
               CheckAttackerConstraints(hit.attacker);
    }

    private new IEnumerator Check(Hit hit)
    {
        if (contextEqualAmount != null)
        {
            var amount = contextEqualAmount.Get(hit.target);
            yield return Run(GetTargets(hit), amount);
        }
        else
        {
            yield return Run(GetTargets(hit), hit.damage + hit.damageBlocked);
        }
    }

    private bool CheckTarget(Entity entity)
    {
        var originalFlags = applyToFlags;
        applyToFlags = whenHitFlags;
        var result = GetTargets().Contains(entity);
        applyToFlags = originalFlags;
        return result;
    }
}