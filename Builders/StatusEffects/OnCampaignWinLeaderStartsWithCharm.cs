#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class OnCampaignWinLeaderStartsWithCharm : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXOnCampaignWin>(Name)
            .WithText("When you defeat the <Frost>, your next <Leader> starts with <+{a}> random charms and charm slots")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnCampaignWin>(status =>
            {
                status.effectToApply = Absent.GetStatus(InstantNewLeadersGetCharms.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
            });
    }
}