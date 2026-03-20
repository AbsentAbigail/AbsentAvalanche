#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenAnAllyGainsAPositiveStatusShareHalfToSelf : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectShareStatus>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectShareStatus>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;

                status.whenAppliedFlags = StatusEffectApplyX.ApplyToFlags.Allies;
                status.negativeStatus = false;
                status.equalAmountBonusMult = -0.49f;
                status.applyEqualAmount = true;
            });
    }
}