using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.Scriptables.TargetConstraints;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class OnHitDamageTargetOn1Counter : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXOnHit>(Name)
            .WithText("Deal <{a}> additional damage to targets on 1<keyword=counter>")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXOnHit>(status =>
            {
                status.doesDamage = true;
                status.addDamageFactor = 1;
                
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Target;
                status.targetConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintDoesAttack>(),
                ];
                status.applyConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintCurrentCounter>("Current Counter is 1"),
                ];
            });
    }
}