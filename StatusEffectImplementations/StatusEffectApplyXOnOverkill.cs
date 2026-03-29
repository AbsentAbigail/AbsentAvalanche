#region

using System.Collections;
using System.Collections.Generic;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectApplyXOnOverkill : StatusEffectApplyX
{
    public ApplyToFlags whenOverkilledFlags = ApplyToFlags.Enemies;
    private readonly List<Hit> _storedHits = [];
    
    public override void Init()
    {
        PostHit += Check;
    }

    public override bool RunPreAttackEvent(Hit hit)
    {
        if (CheckTarget(hit.target))
        {
            _storedHits.Add(hit);
        }
        return false;
    }

    public override bool RunPostHitEvent(Hit hit)
    {
        return Battle.IsOnBoard(target) && _storedHits.Contains(hit) && hit.target.hp.current < 0;
    }

    private IEnumerator Check(Hit hit)
    {
        var currentHp = hit.target.hp.current;
        yield return Run(GetTargets(hit), -currentHp);
        _storedHits.Remove(hit);
    }

    private bool CheckTarget(Entity hitTarget)
    {
        var originalFlags = applyToFlags;
        applyToFlags = whenOverkilledFlags;
        var result = GetTargets().Contains(hitTarget);
        applyToFlags = originalFlags;
        return result;
    }
}