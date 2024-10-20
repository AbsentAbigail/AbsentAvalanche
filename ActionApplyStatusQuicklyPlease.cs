using System.Collections;

namespace AbsentAvalanche;

internal class ActionApplyStatusQuicklyPlease(
    Entity target,
    Entity applier,
    StatusEffectData status,
    int stacks,
    float wait = 0,
    bool temporary = false)
    : ActionApplyStatus(target, applier, status, stacks, temporary)
{
    public override IEnumerator Run()
    {
        yield return StatusEffectSystem.Apply(target, applier, status, stacks, temporary);
        yield return Sequences.Wait(wait);
    }
}