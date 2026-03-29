#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenAllyHitGainEqualSpice : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenAllyIsHitEqualAmount>(Name)
            .WithText("When an ally is hit, gain <keyword=spice> equal to damage taken")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenAllyIsHitEqualAmount>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.effectToApply = Absent.GetStatus("Spice");
                status.applyEqualAmount = true;
            });
    }
}