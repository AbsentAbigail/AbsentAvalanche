#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenDestroyedDealDamageToRandomAlly : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenDestroyed>(Name)
            .WithText("When destroyed, deal <{a}> to a random ally")
            .WithStackable(true)
            .WithCanBeBoosted(true)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenDestroyed>(status =>
            {
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.RandomAlly;
                status.targetMustBeAlive = false;

                status.doesDamage = true;
                status.dealDamage = true;
                status.countsAsHit = true;
            });
    }
}