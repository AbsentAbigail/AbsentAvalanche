using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

internal class OnKillApplyCalmToSelf() : AbstractApplyXStatus<StatusEffectApplyXOnKill>(
    Name, $"Gain <{{a}}> {Keywords.Calm.Tag} on kill",
    canBoost: true,
    effectToApply: Calm.Name
)
{
    public const string Name = "On Kill Apply Calm To Self";
}