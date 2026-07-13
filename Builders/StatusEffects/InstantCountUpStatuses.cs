using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class InstantCountUpStatuses : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectInstantCountUpStatus>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantCountUpStatus>(status =>
            {
                status.negative = true;
                status.positive = true;
            });
    }
}