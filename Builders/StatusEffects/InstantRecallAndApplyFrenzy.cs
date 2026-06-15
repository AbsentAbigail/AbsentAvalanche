using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class InstantRecallAndApplyFrenzy : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectInstantMultiple>(Name)
            .WithText($"<Recall> and apply <x{{a}}><keyword=frenzy>")
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
                    Absent.GetStatusOf<StatusEffectApplyXInstant>("Instant Apply Frenzy (To Card In Hand)"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}