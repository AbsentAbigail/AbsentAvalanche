#region

using System.Collections;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

internal class StatusEffectApplyXWhenEquipped : StatusEffectApplyX
{
    public override void Init()
    {
        OnBegin += Check;
    }

    public override bool RunBeginEvent()
    {
        return !target.data.cardType.item;
    }

    private IEnumerator Check()
    {
        yield return Run(GetTargets());
        yield return Remove();
    }
}