using System.Collections;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Serialization;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectSafeTemporaryTrait : StatusEffectTemporaryTrait
{
    private int _queued;
    private int _finished;

    public override IEnumerator StackRoutine(int stacks)
    {
        var current = _queued;
        _queued++;
        yield return new WaitUntil(() => _finished == current);
        yield return base.StackRoutine(stacks);
        _finished++;
    }
}

public class StatusEffectTriggerWhenAllyAttacksConstrainted : StatusEffectTriggerWhenAllyAttacks
{
  public TargetConstraint[] attackerConstraints;
  
  public override bool RunHitEvent(Hit hit)
  {
    return hit.attacker != null && attackerConstraints.Any(constraint => constraint.Check(hit.attacker)) && base.RunHitEvent(hit);
  }
}