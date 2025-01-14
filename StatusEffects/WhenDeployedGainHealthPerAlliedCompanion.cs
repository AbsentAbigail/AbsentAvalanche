using AbsentAvalanche.StatusEffects.Implementations;
using AbsentAvalanche.StatusEffects.Scriptables;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhenDeployedGainHealthPerAlliedCompanion()
    : AbstractApplyXStatus<StatusEffectApplyXWhenDeployedBoostable>(
        Name, "When deployed, gain <{a}><keyword=health> for each <Companion> on the board",
        canBoost: true,
        effectToApply: "Increase Max Health",
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
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}