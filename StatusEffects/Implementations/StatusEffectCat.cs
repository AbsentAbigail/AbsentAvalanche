using System.Collections;
using AbsentUtilities;
using UnityEngine;
using WildfrostHopeMod.VFX;
using Random = Dead.Random;

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
        for (var cat = 0; cat < count; cat++)
        {
            var duration = 0.1f / Mathf.Max(1, cat / 5);
            var sequence = new ActionSequence(Sequence(hit.target, duration))
            {
                note = name + " - " + cat
            };
            ActionQueue.Insert(cat, sequence, fixedPosition: true);
        }
        yield break;
    }

    private IEnumerator Sequence(Entity applyTo, float duration)
    {
        VFXHelper.VFX.TryPlayEffect("Scratch", applyTo.transform.position, target.transform.lossyScale,
            GIFLoader.PlayType.damageEffect);
        yield return Run([applyTo], 1);
        yield return new WaitForSeconds(duration);
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

        return !canBeBoosted ? count : Mathf.Max(0, Mathf.RoundToInt((count + target.effectBonus) * target.effectFactor));
    }
}