using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class TemporarySafeHogheaded : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectSafeTemporaryTrait>(Name)
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectSafeTemporaryTrait>(status =>
            {
                status.trait = Absent.GetTrait("Pigheaded");
                status.targetConstraints =
                [
                    TargetConstraintHelper.HasTrait("Pigheaded", not: true),
                    TargetConstraintHelper.General<TargetConstraintIsUnit>("Is Not Miniboss",
                        tc => tc.mustBeMiniboss = true, true)
                ];
            });
    }
}