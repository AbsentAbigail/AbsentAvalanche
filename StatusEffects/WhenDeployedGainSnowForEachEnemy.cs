using AbsentAvalanche.StatusEffects.Implementations;
using AbsentAvalanche.StatusEffects.Scriptables;
using AbsentUtilities;
using static StatusEffectApplyX;

namespace AbsentAvalanche.StatusEffects;

internal class WhenDeployedGainSnowForEachEnemy() : AbstractApplyXStatus<StatusEffectApplyXWhenDeployedBoostable>(
    Name,
    canStack: true, canBoost: true,
    applyToFlags: ApplyToFlags.Self,
    subscribe: status => status.scriptableAmount =
        ScriptableHelper.CreateScriptable<ScriptableTargetsOnBoard>("For each enemy", script => script.enemies = true)
)
{
    public const string Name = "When Deployed Gain Snow For Each Enemy";
}