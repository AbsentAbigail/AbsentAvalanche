#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.Scriptables.ScriptableAmounts;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenDeployedGainHealthPerAlliedCompanion : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenDeployedBoostable>(Name)
            .WithText($"When deployed, gain <{{a}}>{Absent.VanillaKeywordTag("health")} for each <Companion> on the board")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDeployedBoostable>(status =>
            {
                status.effectToApply = Absent.GetStatus("Increase Max Health");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.scriptableAmount = new Script<ScriptableTargetsOnBoard>("Allied Companions",
                    scriptable =>
                    {
                        scriptable.cardType = Absent.GetCardType("Friendly");
                        scriptable.allies = true;
                    });
            });
    }
}