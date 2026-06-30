using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenAnythingSnowedTrigger : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenYAppliedTo>(Name)
            .WithText("Trigger when anything is <keyword=snow>'d")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .IsReaction()
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenYAppliedTo>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.effectToApply = Absent.GetStatus("Trigger (High Prio)");

                status.whenAppliedToFlags = StatusEffectApplyX.ApplyToFlags.Allies |
                                            StatusEffectApplyX.ApplyToFlags.Enemies |
                                            StatusEffectApplyX.ApplyToFlags.Hand | StatusEffectApplyX.ApplyToFlags.Self;
                status.whenAppliedTypes = [Absent.GetStatus("Snow").type];
            });
    }
}