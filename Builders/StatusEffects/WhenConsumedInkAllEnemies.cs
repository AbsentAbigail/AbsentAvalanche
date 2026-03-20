#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenConsumedInkAllEnemies : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenDestroyed>(Name)
            .WithText($"When consumed, apply <{{a}}>{Absent.VanillaKeywordTag("null")} to all enemies")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDestroyed>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Enemies;
                status.effectToApply = Absent.GetStatus("Null");

                status.consumed = true;
                status.targetMustBeAlive = false;
            });
    }
}