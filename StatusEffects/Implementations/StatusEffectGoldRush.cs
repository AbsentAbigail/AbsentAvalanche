using System.Collections;
using System.Linq;

namespace AbsentAvalanche.StatusEffects.Implementations;

internal class StatusEffectGoldRush : StatusEffectBonusDamageEqualToX
{
    private bool _removeAttack;

    public override void Init()
    {
        OnEntityDestroyed += Kill;
    }

    public override bool RunCardPlayedEvent(Entity entity, Entity[] targets)
    {
        if (entity != target)
            return false;

        if (targets.Length == 0)
            return false;

        if (ActionQueue.GetActions().Any(a => a is ActionTrigger at && at.entity == target))
            return false;

        if (_removeAttack)
            LoseCurrentAmount();

        _removeAttack = true;
        return false;
    }

    public override bool RunEntityDestroyedEvent(Entity entity, DeathType deathType)
    {
        if (entity.lastHit != null) return entity.lastHit.attacker == target;

        return false;
    }

    private IEnumerator Kill(Entity entity, DeathType deathType)
    {
        _removeAttack = false;

        var num = Find();
        if (num == currentAmount)
            yield break;

        if (toReset)
            LoseCurrentAmount();

        if (num > 0)
            yield return GainAmount(num);
    }

    // Override base class to stop it from Losing current amount
    public override bool RunTurnEndEvent(Entity entity)
    {
        return false;
    }
}