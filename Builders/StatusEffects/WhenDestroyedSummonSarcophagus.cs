#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenDestroyedSummonSarcophagus : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenDestroyed>(Name)
            .WithText("Add <{a}> {0} to hand when destroyed")
            .WithTextInsert("<of the sealed card>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDestroyed>(status =>
            {
                status.effectToApply = Absent.GetStatus(InstantSummonSarcophagus.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.targetMustBeAlive = false;
            });
    }
}