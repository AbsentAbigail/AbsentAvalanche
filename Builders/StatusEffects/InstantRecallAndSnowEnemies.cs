using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class InstantRecallAndSnowEnemies : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectInstantMultiple>(Name)
            .WithText($"<Recall> and apply <x{{a}}><keyword=snow> to all enemies")
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantMultiple>(status =>
            {
                status.effects =
                [
                    Absent.GetStatusOf<StatusEffectInstant>(InstantRecall.Name),
                ];
                status.applyXEffects =
                [
                    Absent.GetStatusOf<StatusEffectApplyXInstant>(InstantApplySnowToAllEnemies.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}