using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class WhileActiveGainFrenzyEqualToBlock() : AbstractApplyXStatus<StatusEffectWhileActiveX>(
    Name, "Has <keyword=frenzy> equal to <keyword=block>",
    effectToApply: "MultiHit",
    subscribe: status =>
    {
        status.scriptableAmount = ScriptableHelper.CreateScriptable<ScriptableCurrentStatus>(
            "Current Block",
            s => s.statusType = AbsentUtils.GetStatus("Block").type
        );
    })
{
    public const string Name = "While Active Gain Frenzy Equal To Block";
}