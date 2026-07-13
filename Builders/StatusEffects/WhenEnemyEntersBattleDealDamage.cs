using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenEnemyEntersBattleDealDamage : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenEnemyEntersBattle>(Name)
            .WithText("When an enemy enters battle, deal <{a}> damage to it")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenEnemyEntersBattle>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Target;
                status.dealDamage = true;
                status.countsAsHit = true;
                status.doesDamage = true;

                status.targetConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintIsUnit>()
                ];
            });
    }
}