#region

using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Patches.StatusFixes;

[HarmonyPatch(typeof(StatusEffectDestroyAfterUse), nameof(StatusEffectDestroyAfterUse.CheckAction))]
public class StatusEffectDestroyAfterUseCheckActionPatches
{
    [UsedImplicitly]
    private static bool Prefix(StatusEffectDestroyAfterUse __instance)
    {
        if (__instance.target.uses.current <= 1)
        {
            return true;
        }
        
        __instance.Unsub();
        return false;
    }
}