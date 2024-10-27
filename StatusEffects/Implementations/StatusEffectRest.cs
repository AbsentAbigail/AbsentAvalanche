using System.Collections;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectRest : StatusEffectApplyX
{
    public void OnDestroy()
    {
        Events.OnActionQueued -= ActionQueued;
    }

    public override void Init()
    {
        Events.OnActionQueued += ActionQueued;
        OnCardPlayed += CardPlayed;
    }

    public override bool RunCardPlayedEvent(Entity entity, Entity[] targets)
    {
        if (entity != target)
            return false;

        return true;
    }

    private IEnumerator CardPlayed(Entity entity, Entity[] targets)
    {
        return Sequence();
    }

    public void ActionQueued(PlayAction playAction)
    {
        if (playAction is not ActionMove actionMove)
            return;

        if (actionMove.entity != target)
            return;

        if (target.owner is null)
            return;

        if (!actionMove.toContainers.Contains(target.owner.discardContainer))
            return;

        ActionQueue.Insert(ActionQueue.IndexOf(playAction), new ActionSequence(Sequence()));
    }

    private IEnumerator Sequence()
    {
        var status = target.FindStatus("ethereal");
        var increase = GetAmount() - (status?.count ?? 0);
        if (increase <= 0)
            yield break;

        yield return Run(GetTargets(), increase);
    }
}