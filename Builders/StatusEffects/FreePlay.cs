using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using WildfrostHopeMod.VFX;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class FreePlay : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectFreePlay>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .Subscribe_WithStatusIcon(Icons.FreePlay.Name)
            .SubscribeToAfterAllBuildEvent<StatusEffectFreePlay>(status =>
            {
                status.eventPriority = -1000; // Negative priority to act after Noomlin/Zoomlin
            });
    }
}