using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenAllyRecalledGainFreePlay : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenCardRecalled>(Name)
            .WithText($"When an ally is recalled, the next card played does not end your turn")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenCardRecalled>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.effectToApply = Absent.GetStatus(FreePlay.Name);

                status.allies = true;
            });
    }
}