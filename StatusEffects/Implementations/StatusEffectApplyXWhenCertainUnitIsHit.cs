namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectApplyXWhenCertainUnitIsHit : StatusEffectApplyXWhenUnitIsHit
{
    public ApplyToFlags whenHitFlags = ApplyToFlags.AllyInFrontOf;

    public override bool RunPostHitEvent(Hit hit)
    {
        if (!CheckTarget(hit.target))
            return false;
        return target.enabled && target.alive && hit.canRetaliate && hit.BasicHit && hit.Offensive &&
               (!targetMustBeAlive || hit.target.alive) && Battle.IsOnBoard(target) && CheckConstraints(hit.target) &&
               CheckAttackerConstraints(hit.attacker);
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