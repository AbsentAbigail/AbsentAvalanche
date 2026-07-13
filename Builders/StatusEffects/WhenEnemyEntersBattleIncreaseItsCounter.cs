using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenEnemyEntersBattleIncreaseItsCounter : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenEnemyEntersBattle>(Name)
            .WithText("When an enemy enters battle, increase its <keyword=counter> by <{a}>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenEnemyEntersBattle>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Target;
                status.effectToApply = Absent.GetStatus("Increase Max Counter");

                status.targetConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintIsUnit>()
                ];
            });
    }
}