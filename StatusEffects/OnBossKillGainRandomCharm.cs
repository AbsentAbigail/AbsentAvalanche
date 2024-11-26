using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

internal class OnBossKillGainRandomCharm() : AbstractApplyXStatus<StatusEffectApplyXOnBossKill>(
    Name,
    canBoost: true,
    effectToApply: InstantGainRandomCharm.Name
)
{
    public const string Name = "On Kill Gain Random Charm";
}