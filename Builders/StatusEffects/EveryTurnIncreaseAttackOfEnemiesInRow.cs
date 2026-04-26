#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class EveryTurnIncreaseAttackOfEnemiesInRow : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXEveryTurn>(Name)
            .WithText("Every turn, increase <keyword=attack> of enemies in row by <{a}>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXEveryTurn>(status =>
            {
                status.effectToApply = Absent.GetStatus("Increase Attack");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.EnemiesInRow;
            });
    }
}