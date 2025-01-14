using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class InstantCountDownEthereal() : AbstractStatus<StatusEffectInstantCountDownStatus>(
    Name, subscribe: status => status.types = ["ethereal"]
    )
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}