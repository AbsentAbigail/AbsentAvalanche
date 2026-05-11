#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenItemPlayedGiveItZoomlin : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXAfterACardPlayed>(Name)
            .WithText($"After a <non->{Absent.VanillaKeywordTag("noomlin")} <Item> is played, give it {Absent.VanillaKeywordTag("zoomlin")}")
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXAfterACardPlayed>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
                status.effectToApply = Absent.GetStatus(InstantQueueTemporaryZoomlin.Name);
                status.allies = true;
                status.constraints =
                [
                    TargetConstraintHelper.IsCardType(["Item"]),
                    TargetConstraintHelper.HasTrait("Noomlin", not: true),
                ];
            });
    }
}