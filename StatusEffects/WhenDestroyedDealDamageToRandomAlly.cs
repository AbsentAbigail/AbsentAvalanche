using AbsentUtilities;
using HarmonyLib;

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
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}