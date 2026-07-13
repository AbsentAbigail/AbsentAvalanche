using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class PreTriggerGainMetronomeBuff : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXPreTrigger>(Name)
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXPreTrigger>(status =>
            {
                status.effectToApply = Absent.GetStatus(InstantMetronome.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.oncePerTurn = false;
            });
    }
}