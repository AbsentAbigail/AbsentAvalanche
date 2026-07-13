using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class InstantApplyLeechSeed : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectInstantApplyEffectAndUpdate>(Name)
            .WithText($"Apply {Absent.KeywordTag(LeechSeed.Name)} <{{a}}>")
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantApplyEffectAndUpdate>(status =>
            {
                status.effectToApply = Absent.GetStatus(EveryTurnGiveHealthToApplier.Name);
                status.targetConstraints =
                [
                    TargetConstraintHelper.HealthMoreThan(0),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}