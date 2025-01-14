using System.Collections;
using System.Linq;
using AbsentUtilities;
using HarmonyLib;
using UnityEngine;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectCamcorder : StatusEffectApplyX
{
    public TargetConstraint[] constraints = [];
    public ApplyToFlags applyFromFlags = ApplyToFlags.Allies;
    
    private int _chainPrevention;
    
    public override void Init()
    {
        PreTrigger += Trigger;
    }

    private IEnumerator Trigger(Trigger trigger)
    {
        if (trigger.entity != target) // if card doesn't have this status, end
            yield break;

        if (TargetSilenced()) // if target is silenced, end
            yield break;

        if (!trigger.countsAsTrigger) // if target isn't full triggering, end (to prevent loops)
            yield break;
        
        if (_chainPrevention > 2) // prevent loops, if it has run three times through this without reaching the end, break
            yield break;
        
        _chainPrevention++;
        
        applyToFlags = applyFromFlags; // Change applyToFlags to get allies
        
        foreach (var ally in GetTargets()) // each ally should attack
        {
            if (!constraints.All(c => c.Check(ally))) // if ally doesn't match constraint, skip
                continue;
            
            foreach (var entity in trigger.targets) // for each attacked enemy
            {
                yield return StatusEffectSystem.Apply(entity, ally, effectToApply, count); // apply trigger against don't count as trigger
            }
        }
        
        _chainPrevention = 0; // reset loop prevention
    }
}