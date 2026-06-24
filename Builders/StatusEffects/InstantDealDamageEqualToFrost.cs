using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class InstantDealDamageEqualToFrost : IStatusBuilder
{
    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectInstantDealDamage>(Name)
            .WithStackable(false)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantDealDamage>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Self;
                
                status.doesDamage = true;
                status.dealDamage = true;
                status.countsAsHit = true;

                status.scriptableAmount = new Script<ScriptableCurrentStatus>("Current Frost",
                    script => script.statusType = Absent.GetStatus("Frost").type);
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}