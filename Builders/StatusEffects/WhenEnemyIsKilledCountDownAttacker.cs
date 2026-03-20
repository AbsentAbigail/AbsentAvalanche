#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class WhenEnemyIsKilledCountDownAttacker : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return Absent.StatusCopy("When Enemy Is Killed Apply Shell To Attacker", Name)
            .WithText($"When an enemy is killed, count down the attackers {Absent.VanillaKeywordTag("counter")} by <{{a}}>")
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenUnitIsKilled>(status =>
            {
                status.effectToApply = Absent.GetStatus(CountDownAsap.Name);
            });
    }
}