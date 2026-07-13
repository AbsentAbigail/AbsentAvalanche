using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class InstantApplyOverburnRemoveAttack : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectInstantMultiple>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantMultiple>(status =>
            {
                status.effects =
                [
                    Absent.GetStatusOf<StatusEffectInstant>(InstantGainApplyOverburn.Name),
                    Absent.GetStatusOf<StatusEffectInstant>("Reduce Attack"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}