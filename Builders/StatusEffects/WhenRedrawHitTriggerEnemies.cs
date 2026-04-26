#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenRedrawHitTriggerEnemies : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenRedrawHit>(Name)
            .WithText("When <Redraw Bell> is hit, trigger all enemies")
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenRedrawHit>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Enemies;
                status.effectToApply = Absent.GetStatus("Trigger");
            });
    }
}