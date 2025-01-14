using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class OnBossKillGainRandomCharm() : AbstractApplyXStatus<StatusEffectApplyXOnBossKill>(
    Name,
    canBoost: true,
    effectToApply: InstantGainRandomCharm.Name
)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}