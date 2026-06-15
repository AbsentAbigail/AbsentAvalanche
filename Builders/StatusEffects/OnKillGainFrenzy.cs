using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class OnKillGainFrenzy : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXOnKill>(Name)
            .WithText("Gain <x{a}><keyword=frenzy> on kill")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnKill>(status =>
            {
                status.effectToApply = Absent.GetStatus("MultiHit");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;

                status.targetConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintDoesKill>()
                ];
            });
    }
}