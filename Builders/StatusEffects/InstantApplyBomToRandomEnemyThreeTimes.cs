using AbsentAvalanche.Builders.Cards.Items;
using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class InstantApplyBomToRandomEnemyThreeTimes : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXInstantMultipleTimes>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXInstantMultipleTimes>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.RandomEnemy;
                status.effectToApply = Absent.GetStatus("Weakness");
            });
    }
}