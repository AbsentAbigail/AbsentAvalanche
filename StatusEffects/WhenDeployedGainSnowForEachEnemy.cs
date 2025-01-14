using AbsentAvalanche.Keywords;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentAvalanche.StatusEffects.Scriptables;
using AbsentUtilities;
using HarmonyLib;
using static StatusEffectApplyX;

namespace AbsentAvalanche.StatusEffects;

internal class WhenDeployedGainSnowForEachEnemy() : AbstractApplyXStatus<StatusEffectApplyXWhenDeployedBoostable>(
    Name, Panic.Tag + " <{a}><keyword=snow>",
    canStack: true, canBoost: true,
    applyToFlags: ApplyToFlags.Self,
    subscribe: status => status.scriptableAmount =
        ScriptableHelper.CreateScriptable<ScriptableTargetsOnBoard>("For each enemy", script => script.enemies = true)
)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}