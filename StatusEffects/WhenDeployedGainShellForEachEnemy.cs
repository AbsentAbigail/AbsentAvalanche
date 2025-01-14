using AbsentAvalanche.Keywords;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentAvalanche.StatusEffects.Scriptables;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class WhenDeployedGainShellForEachEnemy() : AbstractApplyXStatus<StatusEffectApplyXWhenDeployedBoostable>(
    Name, Panic.Tag + " <{a}><keyword=shell>",
    canBoost: true,
    effectToApply: "Shell",
    subscribe: status => status.scriptableAmount =
        ScriptableHelper.CreateScriptable<ScriptableTargetsOnBoard>("For each enemy", script => script.enemies = true)
)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}