using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class InstantGainDealAdditionalDamage : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectInstantAddAttackEffect>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantAddAttackEffect>(status =>
            {
                status.effectToApply = Absent.GetStatus(InstantDealDamage.Name);
                status.targetConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintDoesAttack>()
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}