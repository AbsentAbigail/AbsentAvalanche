using System;
using System.Collections;
using UnityEngine;
using WildfrostHopeMod.Utils;
using WildfrostHopeMod.VFX;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectCat : StatusEffectApplyX
{
    public override void Init()
    {
        PostAttack += Check;
    }

    public override bool RunPostAttackEvent(Hit hit)
    {
        if (hit.attacker != target)
            return false;

        if (hit.nullified)
            return false;

        return hit.target is not null;
    }

    private IEnumerator Check(Hit hit)
    {
        var sequence = new ActionSequence(Sequence(hit.target, 0, count))
        {
            note = name + " - " + 0
        };
        ActionQueue.Stack(sequence, true);

        yield break;
    }

    private IEnumerator Sequence(Entity applyTo, int cat, int max)
    {
        if (!(target.enabled && target.alive))
            yield break;
        
        if (max > 0 && (!applyTo.enabled || !applyTo.alive))
            max = Math.Max(cat - max, -3);

        if (max >= 0 && (cat >= max || max == 0))
            yield break;

        VFXHelper.VFX.TryPlayEffect("Scratch", applyTo.transform.position, target.transform.lossyScale,
            GIFLoader.PlayType.damageEffect);
        yield return Run([applyTo], 1);

        if (max < 0)
            max++;

        yield return new WaitForSeconds(0.1f / Mathf.Max(1, cat / 5));
        var sequence = new ActionSequence(Sequence(applyTo, ++cat, max))
        {
            note = name + " - " + cat
        };
        
        ActionQueue.Stack(sequence, true);
    }

    public override bool TargetSilenced()
    {
        return false;
    }

    public override bool CanTrigger()
    {
        var result = false;
        if (target.enabled)
            result = !affectedBySnow || (!target.IsSnowed && !target.paused);

        if (!result || !dealDamage)
            return effectToApply;

        return true;
    }

    public override int GetAmount()
    {
        if (!target)
            return 0;

        return !canBeBoosted
            ? count
            : Mathf.Max(0, Mathf.RoundToInt((count + target.effectBonus) * target.effectFactor));
    }
}