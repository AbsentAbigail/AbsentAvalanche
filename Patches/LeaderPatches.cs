#region

using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(Leader), "AssignEvent")]
public class LeaderPatches
{
    [UsedImplicitly]
    private static bool Prefix(Leader __instance)
    {
        return __instance.entity.data.TryGetCustomData("CharacterData", out CharacterData _, null);
    }
}