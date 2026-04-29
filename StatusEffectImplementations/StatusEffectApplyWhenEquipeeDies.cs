#region

using AbsentAvalanche.Helpers;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectApplyWhenEquipeeDies : StatusEffectApplyX
{
    private static CardContainer ReserveContainer => References.Player.reserveContainer;
    private bool _trigger;
    private int _amount;
    
    public override void Init()
    {
        Events.OnEntityKilled += Killed;
        Events.OnEntityEnabled += Enabled;
    }

    public void OnDestroy()
    {
        Events.OnEntityKilled -= Killed;
        Events.OnEntityEnabled -= Enabled;
    }

    private void Killed(Entity killedEntity, DeathType deathType)
    {
        if (!ReserveContainer.Contains(target))
        {
            return;
        }
        
        if (killedEntity.FindStatus<StatusEffectEquip>("equip") == null)
        {
            return;
        }

        var equipEffect = target.FindStatus<StatusEffectEquip>("equip");
        if (equipEffect == null)
        {
            return;
        }

        if (equipEffect.cardId != killedEntity.data.id)
        {
            return;
        }

        _trigger = true;
        _amount = scriptableAmount?.Get(killedEntity) ?? 0;
    }

    private void Enabled(Entity entity)
    {
        if (entity != target)
        {
            return;
        }

        if (!_trigger)
        {
            return;
        }

        LogHelper.Log(_amount);
        ActionQueue.Stack(new ActionApplyStatus(target, target, effectToApply, _amount));
        _trigger = false;
    }
}