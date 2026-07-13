using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class EveryTurnGiveHealthToApplier : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXEveryTurn>(Name)
            .WithText($"{Absent.KeywordTag(LeechSeed.Name)} <{{a}}>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXEveryTurn>(status =>
            {
                status.effectToApply = Absent.GetStatus("InstantTakeHpFromApplier");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Applier;
                status.targetConstraints =
                [
                    TargetConstraintHelper.HealthMoreThan(0),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}