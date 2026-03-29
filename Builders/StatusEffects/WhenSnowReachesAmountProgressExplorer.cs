#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenSnowReachesAmountProgressExplorer : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenYAppliedTo>(Name)
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenYAppliedTo>(status =>
            {
                status.effectToApply = Absent.GetStatus(ProgressExplorerHaveSnow.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.whenAppliedToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.whenAppliedTypes = [Absent.GetStatus("Snow").type];
                status.mustReachAmount = true;
            });
    }
}