using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class InstantGainRandomCharm() : AbstractStatus<StatusEffectInstantAddRandomCharm>(
    Name,
    canBoost: true,
    subscribe: status =>
    {
        status.Predicate = charm => charm.scripts is null || charm.scripts.Length == 0;
        status.addToTarget = true;
    })
{
    public const string Name = "Instant Gain Random Charm";
}