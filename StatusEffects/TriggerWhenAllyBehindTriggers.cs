using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class TriggerWhenAllyBehindTriggers() : AbstractStatus<StatusEffectApplyXWhenAllyBehindTriggers>(
    Name, "Trigger when ally behind attacks",
    subscribe: status =>
    {
        status.isReaction = true;
        status.descColorHex = "F99C61";
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}