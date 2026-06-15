using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using WildfrostHopeMod.VFX;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class Flight : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectFlight>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .WithTextInsert("{a}")
            .SubscribeToAfterAllBuildEvent<StatusEffectFlight>(status =>
            {
                status.applyFormatKey = Absent.GetStatus("Shroom").applyFormatKey;
                
                status.targetConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintCanBeHit>("Can be hit")
                ];
            })
            .Subscribe_WithStatusIcon("flight");
    }
}