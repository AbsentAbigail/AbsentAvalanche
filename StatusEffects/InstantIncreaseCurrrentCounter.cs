using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class InstantIncreaseCurrentCounter() : AbstractStatus<StatusInstantIncreaseCounter>(
    Name, "Count <keyword=counter> up by <{a}>",
    canBoost: true,
    subscribe: status => status.type = "counter down"
)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}