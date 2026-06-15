using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenKilledInsteadRecall : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectWhenDestroyedInsteadX>(Name)
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectWhenDestroyedInsteadX>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.effectToApply = Absent.GetStatus(InstantRecall.Name);

                status.resetHealth = true;
                status.ignoreSilence = true;
            });
    }
}