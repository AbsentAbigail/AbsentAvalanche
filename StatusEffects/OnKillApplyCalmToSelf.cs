using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class OnKillApplyCalmToSelf() : AbstractApplyXStatus<StatusEffectApplyXOnKill>(
    Name, $"Gain <{{a}}> {Keywords.Calm.Tag} on kill",
    canBoost: true,
    effectToApply: Calm.Name
)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}