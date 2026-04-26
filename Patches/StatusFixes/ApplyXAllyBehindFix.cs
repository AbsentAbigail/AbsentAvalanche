#region

using System.Collections.Generic;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Patches.StatusFixes;

[HarmonyPatch(typeof(StatusEffectApplyX), nameof(StatusEffectApplyX.GetTargets),
    typeof(Hit),
    typeof(CardContainer[]),
    typeof(CardContainer[]),
    typeof(Entity[]))]
public class ApplyXAllyBehindFix
{
    [UsedImplicitly]
    private static void Postfix(List<Entity> __result, StatusEffectApplyX __instance, 
        Hit hit = null,
        CardContainer[] wasInRows = null,
        CardContainer[] wasInSlots = null,
        Entity[] targets = null)
    {
        if (!__instance.AppliesTo(StatusEffectApplyX.ApplyToFlags.AllyBehind))
        {
            return;
        }
        
        foreach (var cardContainer in wasInSlots ?? __instance.target.actualContainers.ToArray())
        {
            if (cardContainer is not CardSlot cardSlot || cardContainer.Group is not CardSlotLane group)
            {
                continue;
            }
            for (var index = group.slots.IndexOf(cardSlot) + 1; index < group.slots.Count; ++index)
            {
                var entity = group.slots[index].GetTop();
                if (entity is null)
                {
                    continue;
                }

                if (!__result.Contains(entity))
                {
                    __result.Add(entity);   
                }
                break;
            }
        }
    }
}