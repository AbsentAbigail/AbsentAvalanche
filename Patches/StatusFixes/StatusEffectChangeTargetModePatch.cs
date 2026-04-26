#region

using System.Linq;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Patches.StatusFixes;

[HarmonyPatch(typeof(StatusEffectChangeTargetMode), nameof(StatusEffectChangeTargetMode.RunBeginEvent))]
public class StatusEffectChangeTargetModePatch
{
    [UsedImplicitly]
    private static bool Prefix(StatusEffectChangeTargetMode __instance)
    {
        var target = __instance.target;
        var run = !target.statusEffects.Any(e => e is StatusEffectEquip) || !target.data.cardType.item;
        if (!run)
        {
            __instance.pre = Extensions.GetTargetMode("TargetModeBasic");
        }
        return run;
    }
}