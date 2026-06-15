using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class InstantDealDamageEqualToFlightToEnemiesInRow : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectInstantDealDamage>(Name)
            .WithText($"Deal damage equal to its {Absent.KeywordTag("flight")} to enemies in row")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantDealDamage>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.EnemiesInRow;
                status.scriptableAmount = new Script<ScriptableCurrentStatus>(script => script.statusType = "flight");

                status.doesDamage = true;
                status.countsAsHit = true;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}