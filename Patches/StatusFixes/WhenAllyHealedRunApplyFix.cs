#region

using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Patches.StatusFixes;

[HarmonyPatch(typeof(StatusEffectApplyXWhenAllyHealed), nameof(StatusEffectApplyXWhenAllyHealed.RunApplyStatusEvent), typeof(StatusEffectApply))]
public class WhenAllyHealedRunApplyFix
{
    [UsedImplicitly]
    private static bool Prefix(ref bool __result, StatusEffectApplyXWhenAllyHealed __instance, StatusEffectApply apply)
    {
        if (apply.effectData is null || apply.target is null)
        {
            __result = false;
            return false;
        }
        __result = __instance.target.enabled && apply.target != __instance.target && apply.target.owner == __instance.target.owner && apply.effectData.type == "max health up" && Battle.IsOnBoard(__instance.target) && Battle.IsOnBoard(apply.target);
        return false;
    }
}