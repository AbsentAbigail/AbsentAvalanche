using System.Collections;
using AbsentUtilities;

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
        LogHelper.Log("Action queued");
        if (playAction is not ActionMove actionMove)
            return;
        LogHelper.Log("Action move");
        if (actionMove.entity != target)
            return;
        LogHelper.Log("entity target");
        if (target.owner is null)
            return;
        LogHelper.Log("has owner");
        if (!actionMove.toContainers.Contains(target.owner.discardContainer))
            return;
        LogHelper.Log("move to discard container");
        ActionQueue.Insert(ActionQueue.IndexOf(playAction), new ActionSequence(Sequence()));
    }

    private IEnumerator Sequence()
    {
        LogHelper.Log("Sequence");
        var status = target.FindStatus("ethereal");
        var increase = GetAmount() - (status?.count ?? 0);
        if (increase <= 0)
            yield break;
        LogHelper.Log("Increase: " + increase);
        yield return Run(GetTargets(), increase);
    }
}