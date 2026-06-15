using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class InstantDealDamageEqualToAttackToRandomEnemyInRow : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXInstant>(Name)
            .WithText("Deal damage equal to its <keyword=attack> to a random enemy in row")
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXInstant>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.RandomEnemyInRow;
                status.scriptableAmount = new Script<ScriptableCurrentAttack>();

                status.doesDamage = true;
                status.dealDamage = true;
                status.countsAsHit = true;
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}