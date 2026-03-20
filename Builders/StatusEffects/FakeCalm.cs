#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using WildfrostHopeMod.VFX;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class FakeCalm : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenDeployedOnce>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .WithTextInsert("{a}")
            .Subscribe_WithStatusIcon(Icons.Calm.Name)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDeployedOnce>(status =>
            {
                status.effectToApply = Absent.GetStatus(Calm.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
            });
    }
}