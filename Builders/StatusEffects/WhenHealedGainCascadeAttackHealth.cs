#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenHealedGainCascadeAttackHealth : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenHealed>(Name)
            .WithText($"When healed, gain {Absent.KeywordTag(Cascade.Name)} <+{{a}}>{Absent.VanillaKeywordTag("attack")} and <+{{a}}>{Absent.VanillaKeywordTag("health")}")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenHealed>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                status.effectToApply = Absent.GetStatus(InstantCascadingAttackAndHealth.Name);

                status.alsoWhenMaxHealthIncreased = false;
            });
    }
}