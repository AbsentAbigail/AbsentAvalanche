using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(CardUpgradeData), "UnAssign")]
public class CardUpgradeDataPatches
{
    [UsedImplicitly]
    private static void Prefix(CardUpgradeData __instance) //__instance is the instance calling the method
    {
        if (__instance.effectBonus == 0)
            return;

        __instance.effectsAffected ??= [];
        __instance.traitsAffected ??= [];
    }
}