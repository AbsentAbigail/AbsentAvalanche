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
        foreach (var target in targets)
        {
            target.display.promptUpdateDescription = true;
            target.PromptUpdate();
            target.Update();
        }

        yield return Remove();
    }
}