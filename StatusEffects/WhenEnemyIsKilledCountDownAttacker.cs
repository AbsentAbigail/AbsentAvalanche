using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

public class WhenEnemyIsKilledCountDownAttacker() : AbstractApplyXStatus<StatusEffectApplyXWhenUnitIsKilled>(Name)
{
    public const string Name = "WhenEnemyIsKilledCountDownAttacker";
    
    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("When Enemy Is Killed Apply Shell To Attacker", Name)
            .WithText("When an enemy is killed, count down the attackers <keyword=counter> by <{a}>")
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = (StatusEffectApplyXWhenUnitIsKilled)data;
                status.effectToApply = AbsentUtils.GetStatus(CountDownASAP.Name);
            });
    }
}