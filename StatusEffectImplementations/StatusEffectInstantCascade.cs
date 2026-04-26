#region

using System.Collections;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectInstantCascade : StatusEffectApplyXInstant
{
    public override void Init()
    {
        OnBegin += Process;
    }

    private new IEnumerator Process()
    {
        yield return Run(GetTargets());
        yield return Cascade();
        yield return Remove();
    }

    private IEnumerator Cascade()
    {
        var originalFlags = applyToFlags;
        applyToFlags = ApplyToFlags.AllyBehind;
        var allyBehind = GetTargets();
        applyToFlags = originalFlags;

        foreach (var entity in allyBehind)
        {
            yield return StatusEffectSystem.Apply(entity, applier, this, count * 2);
        }
    }
}