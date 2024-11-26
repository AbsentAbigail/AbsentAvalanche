using AbsentAvalanche.Keywords;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentAvalanche.StatusEffects.Scriptables;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

internal class WhenDeployedGainShellForEachEnemy() : AbstractApplyXStatus<StatusEffectApplyXWhenDeployedBoostable>(
    Name, Panic.Tag + " <{a}><keyword=shell>",
    canBoost: true,
    effectToApply: "Shell",
    subscribe: status => status.scriptableAmount =
        ScriptableHelper.CreateScriptable<ScriptableTargetsOnBoard>("For each enemy", script => script.enemies = true)
)
{
    public const string Name = "When Deployed Gain Shell For Each Enemy";
}