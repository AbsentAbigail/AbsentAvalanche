using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class TemporarySafeConsume() : AbstractStatus<StatusEffectSafeTemporaryTrait>(Name, subscribe: status => 
    status.trait = AbsentUtils.GetTrait("Consume"))
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}