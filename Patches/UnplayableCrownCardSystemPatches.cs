#region

using System.Linq;
using AbsentAvalanche.Builders.Traits;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(UnplayableCrownCardSystem), nameof(UnplayableCrownCardSystem.CardIsBlocked),
    [
        typeof(Entity),
        typeof(CardContainer[])
    ])]
public class UnplyableCrownCardSystemPatches
{
    private static string _comboName = Absent.GetTrait(Combo.Name).name;
    [UsedImplicitly]
    private static bool Postfix(bool __result, UnplayableCrownCardSystem __instance, Entity card, CardContainer[] containers) //__instance is the instance calling the method
    {
        if (__result)
        {
            return true;
        }

        if (!card.data.HasCrown)
        {
            return true;
        }
        
        if (card.traits.All(trait => trait.data.name != _comboName))
        {
            return false;
        }

        return card.uses.current < card.uses.max && card.effectBonus > 5;
    }
}

[HarmonyPatch(typeof(UnplayableCrownCardSystem), nameof(UnplayableCrownCardSystem.Check))]
public class UnplyableCrownCardSystemPatchCheck
{
    private static int _counter;
    
    [UsedImplicitly]
    private static void Prefix(UnplayableCrownCardSystem __instance) //__instance is the instance calling the method
    {
        if (++_counter <= 4)
        {
            return;
        }
        
        _counter = 0;
        __instance.handCount++;
    }
}