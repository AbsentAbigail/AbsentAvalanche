#region

using System.Linq;
using AbsentAvalanche.StatusEffectImplementations;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(Entity), nameof(Entity.CanPlayOn), typeof(Entity), typeof(bool))]
internal static class EntityPatches
{
    // Cards on board with cannot be hit shouldn't be able to be targeted
    [UsedImplicitly]
    private static bool Prefix(Entity target)
    {
        return !Battle.IsOnBoard(target) || target.cannotBeHitCount <= 0;
    }

    // Equipment can always be played on a card
    [UsedImplicitly]
    private static void Postfix(ref bool __result, Entity __instance, Entity target)
    {
        if (__instance.statusEffects.Any(statusData => statusData is StatusEffectEquip))
        {
            __result = true;
        }
    }
}