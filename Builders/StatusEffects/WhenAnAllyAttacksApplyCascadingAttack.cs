#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenAnAllyAttacksApplyCascadingAttack : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenAlliesAttack>(Name)
            .WithText($"When an ally attacks, apply {Absent.KeywordTag(Cascade.Name)} <+{{a}}>{Absent.VanillaKeywordTag("attack")} to it")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenAlliesAttack>(status =>
            {
                status.effectToApply = Absent.GetStatus(InstantCascadingAttack.Name);
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Attacker;
                status.queue = true;
            });
    }
}