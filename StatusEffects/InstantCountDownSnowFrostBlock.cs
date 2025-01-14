using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class InstantCountDownSnowFrostBlock() : AbstractStatus<StatusEffectInstantCountDownStatus>(
    Name,
    subscribe: status =>
    {
        status.types = [
            AbsentUtils.GetStatus("Snow").type,
            AbsentUtils.GetStatus("Block").type,
            AbsentUtils.GetStatus("Frost").type
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}