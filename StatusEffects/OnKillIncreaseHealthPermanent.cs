using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class OnKillIncreaseHealthPermanent() : AbstractApplyXStatus<StatusEffectApplyXOnKill>(
    Name, "On kill, <permanently> gain {0}",
    canBoost: true,
    effectToApply: InstantPermanentlyIncreaseHealth.Name,
    subscribe: status => status.textInsert = "<{a}><keyword=health>"
    )
{
    public const string Name = "On Kill Increase Health Permanent";
}