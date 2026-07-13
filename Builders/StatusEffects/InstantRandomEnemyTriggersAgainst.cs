using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class InstantRandomEnemyTriggersAgainst : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXInstant>(Name)
            .WithText("A random ally triggers against the target")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXInstant>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.RandomEnemy;
                status.effectToApply = Absent.GetStatus(InstantTriggerAgainstApplier.Name);
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}