using System.Collections;
using UnityEngine;
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
        for (var cat = 0; cat < count; cat++)
        {
            VFXHelper.VFX.TryPlayEffect("Scratch", hit.target.transform.position, target.transform.lossyScale, GIFLoader.PlayType.damageEffect);
            yield return Run([hit.target], 1);
            yield return new WaitForSeconds(0.1f);
        }
    }
}