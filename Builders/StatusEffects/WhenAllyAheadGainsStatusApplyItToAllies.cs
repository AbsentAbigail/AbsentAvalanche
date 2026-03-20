#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenAllyAheadGainsStatusApplyItToAllies : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectShareStatus>(Name)
            .WithText("When ally in front gains a positive status, share it to all other allies")
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectShareStatus>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;

                status.whenAppliedFlags = StatusEffectApplyX.ApplyToFlags.AllyInFrontOf;
                status.negativeStatus = false;
                status.applyEqualAmount = true;
            });
    }
}