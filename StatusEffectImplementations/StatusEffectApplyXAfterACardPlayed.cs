#region

using System.Collections;
using System.Linq;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;


internal class StatusEffectApplyXAfterACardPlayed : StatusEffectApplyX
{
    public TargetConstraint[] constraints;
    public bool includeSelf;
    public bool allies;
    
    public override void Init()
    {
        OnCardPlayed += Check;
    }

    private IEnumerator Check(Entity entity, Entity[] targets)
    {
        yield return Run(GetTargets(new Hit(entity, null)));
    }

    public override bool RunCardPlayedEvent(Entity entity, Entity[] targets)
    {
        if (target.silenced)
            return false;

        if (!includeSelf && entity == target)
        {
            return false;
        }
        
        if (allies && entity.owner != target.owner)
        {
            return false;
        }
        
        if (constraints.Any(constraint => !constraint.Check(entity)))
        {
            return false;
        }
        
        var hasQueuedTriggers = ActionQueue.GetActions()
            .Any(playAction => playAction is ActionTrigger actionTrigger && actionTrigger.entity == entity);
        return !hasQueuedTriggers;
    }
}