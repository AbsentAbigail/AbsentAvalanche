using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class OnKillIncreaseHealthPermanent() : AbstractApplyXStatus<StatusEffectApplyXOnKill>(
    Name, "On kill, <permanently> gain {0}",
    canBoost: true,
    effectToApply: InstantPermanentlyIncreaseHealth.Name,
    subscribe: status => status.textInsert = "<{a}><keyword=health>"
    )
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}