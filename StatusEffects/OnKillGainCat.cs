using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class OnKillGainCat() : AbstractApplyXStatus<StatusEffectApplyXOnKill>(
    Name, $"Gain <{{a}}> {Keywords.Cat.Tag} on kill",
    canBoost: true,
    effectToApply: Cat.Name
)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}