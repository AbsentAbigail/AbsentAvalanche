using AbsentUtilities;
using HarmonyLib;

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
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}