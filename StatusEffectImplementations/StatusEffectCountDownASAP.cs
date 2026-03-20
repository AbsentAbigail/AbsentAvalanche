#region

using System.Collections;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectCountDownAsap : StatusEffectData
{
    public StatusEffectData effectToApply;
    private bool _activated;

    public void OnDestroy()
    {
        Events.OnEntityDisplayUpdated -= EntityDisplayUpdated;
    }

    public override void Init()
    {
        Events.OnEntityDisplayUpdated += EntityDisplayUpdated;
        TryActivate();
    }


    private void EntityDisplayUpdated(Entity entity)
    {
        if (entity == null || entity != target)
            return;
        TryActivate();
    }

    private void TryActivate()
    {
        if (_activated || target.counter.current <= 0)
            return;
        _activated = true;
        ActionQueue.Stack(new ActionSequence(Activate()) { note = "Count down ASAP" });
    }

    private IEnumerator Activate()
    {
        yield return StatusEffectSystem.Apply(target, applier, effectToApply, count);
        yield return Remove();
    }
}