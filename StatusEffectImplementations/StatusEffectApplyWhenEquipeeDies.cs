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

        if (target.data.GetCustomDataOrNull("absent.equipment") is not ulong equipmentId)
        {
            return;
        }
        
        if (killedEntity.data.GetCustomDataOrNull("absent.equipments") is not SaveCollection<ulong> equipmentIds)
        {
            return;
        }

        if (!equipmentIds.collection.Contains(equipmentId))
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