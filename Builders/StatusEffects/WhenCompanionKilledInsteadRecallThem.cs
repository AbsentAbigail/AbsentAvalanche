using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenCompanionKilledInsteadRecallThem : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectWhileActiveX>(Name)
            .WithText($"When a <Companion> is killed, instead <recall> them")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectWhileActiveX>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
                status.effectToApply = Absent.GetStatus(WhenKilledInsteadRecall.Name);

                status.applyConstraints =
                [
                    TargetConstraintHelper.IsCardType(["Friendly"])
                ];
            });
    }
}