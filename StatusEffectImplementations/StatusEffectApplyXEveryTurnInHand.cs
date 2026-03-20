#region

using System.Collections;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectApplyXEveryTurnInHand : StatusEffectApplyX
{
    public override void Init()
    {
        OnTurnEnd += Check;
    }

    public override bool RunTurnEndEvent(Entity entity)
    {
        if (!target.enabled)
            return false;

        if (entity != target.owner.entity)
            return false;

        return target.InHand();
    }

    private IEnumerator Check(Entity entity)
    {
        yield return Run(GetTargets());
    }
}