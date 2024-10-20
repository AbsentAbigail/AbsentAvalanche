using System.Collections;
using System.Linq;

namespace AbsentAvalanche.StatusEffects.Implementations;

internal class StatusEffectApplyXAfterCardPlayed : StatusEffectApplyX
{
    public override void Init()
    {
        OnCardPlayed += Check;
    }

    private IEnumerator Check(Entity entity, Entity[] targets)
    {
        yield return Run(GetTargets());
    }

    public override bool RunCardPlayedEvent(Entity entity, Entity[] targets)
    {
        if (entity != target)
            return false;

        if (target.silenced)
            return false;

        var hasQueuedTriggers = ActionQueue.GetActions()
            .Any(a => a as ActionTrigger != null && (a as ActionTrigger).entity == target);
        if (hasQueuedTriggers)
            return false;

        return true;
    }
}