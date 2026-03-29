#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenShellReachesAmountProgressExplorer : IStatusBuilder
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
                status.effectToApply = Absent.GetStatus(ProgressExplorerHaveShell.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.whenAppliedToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.whenAppliedTypes = [Absent.GetStatus("Shell").type];
                status.mustReachAmount = true;
            });
    }
}