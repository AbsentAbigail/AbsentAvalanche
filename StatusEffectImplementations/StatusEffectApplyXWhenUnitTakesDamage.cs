#region

using System.Collections;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectApplyXWhenUnitTakesDamage : StatusEffectApplyXWhenUnitIsHit
{
    public bool allies = false;
    public bool enemies = true;

    public override void Init()
    {
        PostHit += Check;
    }

    public override bool RunPostHitEvent(Hit hit)
    {
        if (!CheckTarget(hit.target))
            return false;
        return target.enabled && target.alive && hit.Offensive &&
               (!targetMustBeAlive || hit.target.alive && Battle.IsOnBoard(hit.target)) && Battle.IsOnBoard(target)
               && CheckConstraints(hit.target) && CheckAttackerConstraints(hit.attacker);
    }

    private new IEnumerator Check(Hit hit)
    {
        var amount = contextEqualAmount?.Get(hit.target) ?? hit.damage + hit.damageBlocked;
        yield return Run(GetTargets(hit), amount);
    }

    private bool CheckTarget(Entity entity)
    {
        if (allies && target.owner == entity.owner)
        {
            return true;
        }
        return enemies && target.owner != entity.owner;
    }
}