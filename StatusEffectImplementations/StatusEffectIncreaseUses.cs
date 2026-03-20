#region

using System;
using System.Collections;
using System.Linq;
using AbsentAvalanche.StatusEffectImplementations.Actions;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

internal class StatusEffectIncreaseUses : StatusEffectData
{
    private int _baseUses;

    public override object GetMidBattleData()
    {
        return _baseUses;
    }

    public override void RestoreMidBattleData(object data)
    {
        if (data is int baseUses) _baseUses = baseUses;
    }

    public override bool RunBeginEvent()
    {
        _baseUses = target.uses.max;

        return false;
    }

    public override bool RunStackEvent(int stacks)
    {
        IncreaseUses();
        ActionQueue.Add(new ActionUpdateText(target));

        var consume = (StatusEffectDestroyAfterUse)target.statusEffects.Find(s => s is StatusEffectDestroyAfterUse
        {
            destroy: true
        });
        consume?.destroy = false;
        consume?.Unsub();

        return false;
    }

    public override bool RunCardMoveEvent(Entity entity)
    {
        if (entity != target) return false;

        entity.display.promptUpdateDescription = true;
        entity.PromptUpdate();
        entity.Update();
        return false;
    }

    public override bool RunCardPlayedEvent(Entity entity, Entity[] targets)
    {
        if (entity != target) return false;
        if (!ActionQueue.GetActions()
                .Any(action => action is ActionUpdateText textAction && textAction.entity == target))
            ActionQueue.Add(new ActionUpdateText(target));

        return false;
    }

    public override bool RunPreTriggerEvent(Trigger trigger)
    {
        if (trigger.entity != target) return false;

        IncreaseUses();

        return false;
    }

    public override IEnumerator RemoveStacks(int amount, bool removeTemporary)
    {
        target.uses.max = _baseUses;
        target.uses.current = Math.Min(target.uses.current, _baseUses);
        return base.RemoveStacks(amount, removeTemporary);
    }

    private void IncreaseUses()
    {
        var newMax = _baseUses + GetAmount();
        var change = newMax - target.uses.max;
        target.uses.max = newMax;
        if (change > 0)
            target.uses.current += change;
        else
            target.uses.current = Math.Min(target.uses.current, newMax);
    }
}