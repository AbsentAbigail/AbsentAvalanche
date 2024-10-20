using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class OnKillTriggerNoTrigger() : AbstractApplyXStatus<StatusEffectApplyXOnKill>(
    Name,
    effectToApply: TriggerNoTrigger.Name,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.Self
)
{
    public const string Name = "On Kill Trigger No Trigger";
}