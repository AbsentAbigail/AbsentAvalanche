using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class InstantReplaceAttackWithOverburn : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXInstant>(Name)
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXInstant>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.scriptableAmount = new Script<ScriptableCurrentAttack>(null);
                status.effectToApply = Absent.GetStatus(InstantApplyOverburnRemoveAttack.Name);
                status.targetConstraints =
                [
                    TargetConstraintHelper.AttackMoreThan(0)
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}