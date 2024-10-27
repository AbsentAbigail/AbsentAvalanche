using AbsentAvalanche.StatusEffects.Implementations;
using AbsentAvalanche.StatusEffects.Scriptables;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class WhenDeployedReduceCounterPerAlliedCompanion()
    : AbstractApplyXStatus<StatusEffectApplyXWhenDeployedBoostable>(
        Name, "When deployed, reduce own <keyword=counter> by <{a}> for each allied <Companion>",
        canBoost: true,
        effectToApply: "Reduce Max Counter",
        applyToFlags: StatusEffectApplyX.ApplyToFlags.Self,
        subscribe: status =>
        {
            status.scriptableAmount = ScriptableHelper.CreateScriptable<ScriptableTargetsOnBoard>(
                "Allied Companions",
                scriptable =>
                {
                    scriptable.cardType = AbsentUtils.TryGet<CardType>("Friendly");
                    scriptable.allies = true;
                });
        })
{
    public const string Name = "When Deployed Reduce Counter Per Allied Companion";
}