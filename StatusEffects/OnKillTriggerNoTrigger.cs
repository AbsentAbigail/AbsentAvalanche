using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class OnKillTriggerNoTrigger() : AbstractApplyXStatus<StatusEffectApplyXOnKill>(
    Name,
    effectToApply: TriggerNoTrigger.Name,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.Self
)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}