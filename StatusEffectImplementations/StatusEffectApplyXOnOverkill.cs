#region

using System.Collections;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectApplyXOnOverkill : StatusEffectApplyX
{
    public bool allies = false;
    public bool enemies = true;
    public string damageType = null;
    
    public override void Init()
    {
        PostHit += Check;
    }

    public override bool RunPostHitEvent(Hit hit)
    {
        return Battle.IsOnBoard(target) && CheckTarget(hit.target) && CheckDamageType(hit.damageType) && hit.target.hp.current < 0;
    }

    private IEnumerator Check(Hit hit)
    {
        var currentHp = hit.target.hp.current;
        yield return Run(GetTargets(hit), -currentHp);
    }

    private bool CheckDamageType(string hitDamageType)
    {
        return damageType.IsNullOrEmpty() || damageType.Equals(hitDamageType);
    }
    
    private bool CheckTarget(Entity hitTarget)
    {
        if (allies && hitTarget.owner == target.owner)
        {
            return true;
        }

        return enemies && hitTarget.owner != target.owner;
    }
}