using System.Collections;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectTriggerWithoutFrenzy : StatusEffectInstant
{
    public bool againstRandomEnemy;
    public bool reduceUses;
    public int priority = -1;

    public override IEnumerator Process()
    {
        if (againstRandomEnemy && target.NeedsTarget)
        {
            var allEnemies = target.GetAllEnemies();
            if (allEnemies.Count > 0)
            {
                var randomTarget = allEnemies.RandomItem();
                var targetContainer = randomTarget.containers.RandomItem();
                ActionQueue.Stack(new ActionTriggerAgainst(randomTarget, applier, randomTarget, targetContainer)
                {
                    countsAsTrigger = false
                }, true);
            }
        }
        else
        {
            var action = new ActionTriggerNoFrenzy(target, applier)
            {
                priority = priority
            };
            ActionQueue.Stack(action, true);
        }

        if (reduceUses)
            ActionQueue.Add(new ActionReduceUses(target));

        yield return base.Process();
    }

    private class ActionTriggerNoFrenzy(Entity entity, Entity triggeredBy) : ActionTrigger(entity, triggeredBy)
    {
        public override Trigger GetTrigger()
        {
            var trigger = base.GetTrigger();
            trigger.countsAsTrigger = false;
            return trigger;
        }
    }
}