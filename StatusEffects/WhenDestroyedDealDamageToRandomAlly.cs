using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class WhenDestroyedDealDamageToRandomAlly() : AbstractApplyXStatus<StatusEffectApplyXWhenDestroyed>(
    Name, "When destroyed, deal <{a}> to a random ally",
    canBoost: true,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.RandomAlly,
    subscribe: status =>
    {
        status.targetMustBeAlive = false;
        
        status.doesDamage = true;
        status.dealDamage = true;
        status.countsAsHit = true;
    })
{
    public const string Name = "When Destroyed Deal Damage To RandomAlly";
}