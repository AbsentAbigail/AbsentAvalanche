using System.Collections;
using HarmonyLib;
using UnityEngine;

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectInstantSummonRandomFromPool : StatusEffectInstantSummon
{
    public CardData[] pool;
    public CardUpgradeData[] withScripts;
    public new StatusEffectSummonWithCrown targetSummon;

    public override IEnumerator Process()
    {
        Routine.Clump clump = new();
        var amount = GetAmount();
        for (var i = 0; i < amount; i++)
        {
            if (pool.Length > 0)
            {
                var card = pool.RandomItem().Clone();
                withScripts.Do(script => script.Clone().Assign(card));
                targetSummon.summonCard = card;
                
            }
            clump.Add(TrySummon());
            yield return clump.WaitForEnd();
        }

        yield return Remove();
    }
    
    public bool ToSummon() => toSummon;
    public new IEnumerator TrySummon()
    {
        if (buildingToSummon)
        {
            yield return new WaitUntil(ToSummon);
        }

        if (CanSummon(out var container, out var shoveData))
        {
            if (shoveData != null)
                yield return ShoveSystem.DoShove(shoveData, true);
            var amount = GetAmount();
            yield return toSummon ? targetSummon.SummonPreMade(toSummon, container, applier.display.hover.controller, applier, withEffects, amount) : summonCopy ? targetSummon.SummonCopy(target, container, applier.display.hover.controller, applier, withEffects, amount) : (object) targetSummon.Summon(container, applier.display.hover.controller, applier, withEffects, amount);
        }
        else if (NoTargetTextSystem.Exists())
        {
            if (toSummon)
            {
                toSummon.RemoveFromContainers();
                Destroy(toSummon);
            }
            yield return NoTargetTextSystem.Run(target, NoTargetType.NoSpaceToSummon);
        }
        yield return null;
    }
}