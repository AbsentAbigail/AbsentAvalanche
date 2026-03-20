#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenAnythingSnowedHealAllies : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenYAppliedTo>(Name)
            .WithText(
                $"When anything is {Absent.VanillaKeywordTag("snow")}'d, restore <{{a}}>{Absent.VanillaKeywordTag("health")} to all allies")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenYAppliedTo>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
                status.effectToApply = Absent.GetStatus("Heal");

                status.whenAppliedToFlags = StatusEffectApplyX.ApplyToFlags.Allies |
                                            StatusEffectApplyX.ApplyToFlags.Enemies |
                                            StatusEffectApplyX.ApplyToFlags.Hand | StatusEffectApplyX.ApplyToFlags.Self;
                status.whenAppliedTypes = [Absent.GetStatus("Snow").type];
            });
    }
}