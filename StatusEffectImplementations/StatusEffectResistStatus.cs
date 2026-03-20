#region

using System;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectResistStatus : StatusEffectApplyX
{
    public int maxAmount = 1;
    public string[] resistTypes;

    public override bool RunApplyStatusEvent(StatusEffectApply apply)
    {
        if (!Battle.instance) return false;
        if (!Battle.IsOnBoard(target)) return false;

        if (!GetTargets().Contains(apply.target)) return false;

        var applyType = apply.effectData.type;

        if (!resistTypes.Contains(applyType)) return false;

        var targetEffectCount = apply.target.FindStatus(applyType)?.count ?? 0;

        apply.count = Math.Max(maxAmount - targetEffectCount, 0);

        return false;
    }
}