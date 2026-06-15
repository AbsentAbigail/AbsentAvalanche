using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenRecalledDealDamageToEnemiesInRow : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenCardRecalled>(Name)
            .WithText($"When <recalled>, deal <{{a}}> damage to enemies in row")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenCardRecalled>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.EnemiesInRow;
                status.doesDamage = true;
                status.dealDamage = true;
                status.countsAsHit = true;

                status.self = true;
                status.onBoard = true;
            });
    }
}