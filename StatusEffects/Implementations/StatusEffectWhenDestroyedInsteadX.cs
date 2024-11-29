using System.Collections;
using System.Linq;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectWhenDestroyedInsteadX : StatusEffectApplyX
{
    public bool resetHealth;

    private bool _activated;
    private int _lastHealth;
    private bool _lastScrap;

    public void OnDestroy()
    {
        Events.OnEntityDisplayUpdated -= EntityDisplayUpdated;
    }

    public override void Init()
    {
        Events.OnEntityDisplayUpdated += EntityDisplayUpdated;
        preventDeath = true;
    }

    public void EntityDisplayUpdated(Entity entity)
    {
        if (!_activated && target.hp.current <= 0 && entity == target)
            TryActivate();
        UpdateHealth();
    }

    public override bool RunPostHitEvent(Hit hit)
    {
        if (!_activated && hit.target == target && target.hp.current <= 0)
            TryActivate();
        UpdateHealth();
        return false;
    }

    public void TryActivate()
    {
        if (target.statusEffects.Any(s =>
                s.preventDeath && 
                s is not StatusEffectWhenDestroyedInsteadX &&
                s is not StatusEffectNextPhase)
            ) return;
        
        _activated = true;

        if (resetHealth)
        {
            target.hp.current = _lastHealth;
            if (_lastScrap)
                ActionQueue.Stack(new ActionApplyStatus(target, target, AbsentUtils.GetStatus("Scrap"), 1), true);
        }

        ActionQueue.Stack(new ActionSequence(Run(GetTargets())));
        ActionQueue.Stack(new ActionSequence(RemoveAndUpdateDescription()));
    }

    private IEnumerator RemoveAndUpdateDescription()
    {
        yield return Remove();

        target.display.promptUpdateDescription = true;
        target.PromptUpdate();
    }

    private void UpdateHealth()
    {
        if (!resetHealth)
            return;
        _lastHealth = target.hp.current;
        _lastScrap = (bool)target.FindStatus("scrap");
    }
}