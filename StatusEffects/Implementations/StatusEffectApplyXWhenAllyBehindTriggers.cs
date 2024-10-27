namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectApplyXWhenAllyBehindTriggers : StatusEffectTriggerWhenAllyAttacks
{
    public bool allyBehind = true;
    public bool allyInFront;

    public override void Init()
    {
        allyInRow = true;
        base.Init();
    }

    public override bool RunHitEvent(Hit hit)
    {
        if (target.enabled && Battle.IsOnBoard(target) && hit.countsAsHit && hit.Offensive && hit.target &&
            hit.trigger != null && CheckEntity(hit.attacker))
            prime.Add(hit.attacker);
        return false;
    }

    private new bool CheckEntity(Entity entity)
    {
        var result = base.CheckEntity(entity);

        if (entity.triggeredBy == target)
            return false;

        result &= (allyInFront && IsInFront(entity)) || (allyBehind && IsBehind(entity));

        return result;
    }

    private bool IsInFront(Entity entity)
    {
        foreach (var cardContainer in target.actualContainers.ToArray())
        {
            if (cardContainer is not CardSlot cardSlot || cardContainer.Group is not CardSlotLane group) continue;

            var index = group.slots.IndexOf(cardSlot);
            if (--index >= 0) continue;
            if (group.slots[index].GetTop() == entity)
                return true;
        }

        return false;
    }

    private bool IsBehind(Entity entity)
    {
        foreach (var cardContainer in target.actualContainers.ToArray())
        {
            if (cardContainer is not CardSlot cardSlot || cardContainer.Group is not CardSlotLane group) continue;

            var index = group.slots.IndexOf(cardSlot);
            if (++index >= group.slots.Count) continue;
            if (entity == group.slots[index].GetTop())
                return true;
        }

        return false;
    }
}