#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenRedrawHitApplyBomToEnemiesInRow : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenRedrawHit>(Name)
            .WithText($"When redraw bell is hit, apply <{{a}}>{Absent.VanillaKeywordTag("weakness")} to enemies in row")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenRedrawHit>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.EnemiesInRow;
                status.effectToApply = Absent.GetStatus("Weakness");
            });
    }
}