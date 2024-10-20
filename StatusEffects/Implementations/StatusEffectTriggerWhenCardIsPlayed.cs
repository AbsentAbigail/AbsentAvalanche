using System.Linq;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects.Implementations;

internal class StatusEffectTriggerWhenCardIsPlayed : StatusEffectTriggerWhenAllyAttacks
{
    public CardData[] whenCardsPlayed;

    private readonly StatusEffectData _triggerEffect =
        AbsentUtils.GetStatusOf<StatusEffectInstantTriggerAgainst>("Trigger Against & Reduce Uses");

    private bool _isPlayed;
    private bool _primed;

    public override void Init()
    {
        base.Init();
        allyInRow = false;
        againstTarget = true;
    }

    public override bool RunActionPerformedEvent(PlayAction action)
    {
        if (!ActionQueue.Empty) return base.RunActionPerformedEvent(action);

        _primed = false;
        _isPlayed = false;

        return base.RunActionPerformedEvent(action);
    }

    public override bool RunHitEvent(Hit hit)
    {
        if (hit.attacker is null)
            return false;

        if (_primed)
            return false;

        if (!References.Player.handContainer.Contains(target))
            return false;

        if (whenCardsPlayed.All(cardData => cardData.name != hit.attacker.data.name))
            return false;

        if (target.enabled && (bool)hit.target && hit.trigger != null && CheckEntity(hit.attacker))
            _primed = true;

        return false;
    }

    public override bool RunCardPlayedEvent(Entity entity, Entity[] targets)
    {
        if (entity == target)
        {
            _isPlayed = true;
            return false;
        }

        if (!_primed || _isPlayed || targets is not { Length: > 0 })
            return false;

        _isPlayed = true;
        if (CanTrigger())
            Run(targets);

        return false;
    }

    public void Run(Entity[] targets)
    {
        foreach (var entity in targets)
            ActionQueue.Stack(new ActionApplyStatusQuicklyPlease(entity, target, _triggerEffect, 1));
    }

    private new bool CheckEntity(Entity entity)
    {
        if (!entity)
            return false;

        if (entity.owner.team != target.owner.team)
            return false;

        return entity != target && CheckRow(entity);
    }
}