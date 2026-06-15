using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.Scriptables.SelectScripts;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenDeployedApplyBomToFurthestEnemies : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenDeployed>(Name)
            .WithText($"When deployed, apply <{{a}}><keyword=weakness> to the furthest enemies")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDeployed>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Enemies;
                status.effectToApply = Absent.GetStatus("Weakness");
                status.selectScript = new Script<SelectScriptFurthestEnemies>();
            });
    }
}