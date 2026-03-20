#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenAllyGainsNegativeStatusApplyToSelfInstead : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectWhenXAppliedToRedirect>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectWhenXAppliedToRedirect>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;

                status.positiveStatus = false;
                status.whenAppliedFlags = StatusEffectApplyX.ApplyToFlags.Allies;
                status.applyEqualAmount = true;
            });
    }
}