#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class AfterAllyAttacksApplyCascadingAttack : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXAfterACardPlayed>(Name)
            .WithText($"After an ally or self attacks, apply {Absent.KeywordTag(Cascade.Name)} <+{{a}}>{Absent.VanillaKeywordTag("attack")} to it")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXAfterACardPlayed>(status =>
            {
                status.effectToApply = Absent.GetStatus(InstantCascadingAttack.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
                status.constraints =
                [
                    TargetConstraintHelper.General<TargetConstraintDoesDamage>(),
                    TargetConstraintHelper.IsCardType(["Item"], not: true)
                ];
                status.allies = true;
                status.includeSelf = true;
                status.queue = true;
            });
    }
}