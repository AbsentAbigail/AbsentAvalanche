#region

using System.Collections;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

internal class StatusEffectApplyXInstantAndUpdate : StatusEffectApplyXInstant
{
    public override void Init()
    {
        OnBegin += Process;
    }

    public new IEnumerator Process()
    {
        var targets = GetTargets();
        yield return Run(targets);
        foreach (var entity in targets)
        {
            entity.display.promptUpdateDescription = true;
            entity.PromptUpdate();
        }

        yield return Remove();
    }
}