using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

internal class OnKillGainCat() : AbstractApplyXStatus<StatusEffectApplyXOnKill>(
    Name, $"Gain <{{a}}> {Keywords.Cat.Tag} on kill",
    canBoost: true,
    effectToApply: Cat.Name
)
{
    public const string Name = "On Kill Gain Cat";
}