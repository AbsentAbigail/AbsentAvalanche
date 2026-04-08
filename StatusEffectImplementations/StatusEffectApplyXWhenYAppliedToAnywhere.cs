#region

using UnityEngine;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectApplyXWhenYAppliedToAnywhere : StatusEffectApplyXWhenYAppliedTo
{
  public override void Init()
  {
    PostApplyStatus += Run;
  }

  public override bool RunApplyStatusEvent(StatusEffectApply apply)
  {
    if ((!adjustAmount && !instead) || !target.enabled || TargetSilenced() || (!target.alive && targetMustBeAlive) ||
        !(bool)(Object)apply.effectData || apply.count <= 0 || !CheckType(apply.effectData) ||
        !CheckTarget(apply.target))
    {
      return false;
    }

    if (instead)
    {
      apply.effectData = effectToApply;
    }

    if (!adjustAmount)
    {
      return false;
    }
    apply.count += addAmount;
    apply.count = Mathf.RoundToInt(apply.count * multiplyAmount);
    return false;
  }

  public override bool RunPostApplyStatusEvent(StatusEffectApply apply)
  {
    return target.enabled && !TargetSilenced() && (bool) (Object) apply.effectData && apply.count > 0 && CheckType(apply.effectData) && CheckTarget(apply.target) && CheckAmount(apply);
  }

  private new bool CheckTarget(Entity entity)
  {
    if (entity == target)
      return CheckFlag(ApplyToFlags.Self);
    if (entity.owner == target.owner)
      return CheckFlag(ApplyToFlags.Allies);
    return entity.owner != target.owner && CheckFlag(ApplyToFlags.Enemies);
  }
}