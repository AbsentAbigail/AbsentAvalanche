#region

using HarmonyLib;

#endregion

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(Entity), nameof(Entity.CanPlayOn), typeof(Entity), typeof(bool))]
internal static class EntityPatches
{
    // Cards on board with cannot be hit shouldn't be able to be targeted
    private static bool Prefix(Entity target)
    {
        return !Battle.IsOnBoard(target) || target.cannotBeHitCount <= 0;
    }
}