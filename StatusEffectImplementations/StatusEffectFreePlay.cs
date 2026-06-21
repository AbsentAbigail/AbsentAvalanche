using System.Collections;
using AbsentAvalanche.StatusEffectImplementations.Actions;

namespace AbsentAvalanche.StatusEffectImplementations;

internal class StatusEffectFreePlay : StatusEffectData
{
    public override void Init()
    {
        OnCardPlayed += Check;
        OnCardMove += Check;
        Events.OnActionFinished += CheckEquip;
    }

    private void OnDestroy()
    {
        Events.OnActionFinished -= CheckEquip;
    }

    private void CheckEquip(PlayAction action)
    {
        if (Battle.instance.phase == Battle.Phase.Init)
        {
            return;
        }

        if (action is not ActionEquip equip)
        {
            return;
        }

        var equipment = equip.equipment;
        
        if (!Battle.IsOnBoard(target) || target.owner != equipment.owner)
        {
            return;
        }
        
        if (target.owner.freeAction)
        {
            return;
        }

        ActionQueue.Add(new ActionSequence(Check(null, null)));
    }

    public override bool RunCardPlayedEvent(Entity entity, Entity[] targets)
    {
        if (Battle.instance.phase == Battle.Phase.Init)
        {
            return false;
        }
        
        if (!Battle.IsOnBoard(target) || target.owner != entity.owner)
        {
            return false;
        }
        
        return References.Player.handContainer.Contains(entity) && !entity.owner.freeAction;
    }

    public override bool RunCardMoveEvent(Entity entity)
    {
        if (Battle.instance.phase == Battle.Phase.Init)
        {
            return false;
        }

        if (!Battle.IsOnBoard(target) || entity == target || target.owner != entity.owner)
        {
            return false;
        }
        
        return !Battle.IsOnBoard(entity.preContainers) && Battle.IsOnBoard(entity) && !entity.owner.freeAction;
    }

    private IEnumerator Check(Entity entity)
    {
        return Check(entity, null);
    }

    private IEnumerator Check(Entity entity, Entity[] targets)
    {
        target.owner.freeAction = true;
        yield return CountDown(target, 1);
    }
}