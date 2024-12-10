using System.Collections;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectCountDownAsap : StatusEffectData
{
    public StatusEffectData effectToApply;
    private bool activated;
    
    public void OnDestroy()
    {
        Events.OnEntityDisplayUpdated -= EntityDisplayUpdated;
    }

    public override void Init()
    {
        LogHelper.Log("Count down asap initiated");
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
        if (activated || target.counter.current <= 0)
            return;
        activated = true;
        ActionQueue.Stack(new ActionSequence(Activate()) { note = "Count down ASAP" });
    }

    private IEnumerator Activate()
    {
        LogHelper.Log("Count down asap applied");
        yield return StatusEffectSystem.Apply(target, applier, effectToApply, count);
        yield return Remove();
    }
}