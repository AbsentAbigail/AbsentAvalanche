#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenAllyHealedCleanse : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenAllyHealed>(Name)
            .WithText($"When an ally is healed, {Absent.VanillaKeywordTag("cleanse")} them")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenAllyHealed>(status =>
            {
                status.effectToApply = Absent.GetStatus("Cleanse");
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Target;
            });
    }
}