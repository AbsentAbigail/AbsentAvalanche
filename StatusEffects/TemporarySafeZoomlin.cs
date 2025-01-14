using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class TemporarySafeZoomlin() : AbstractStatus<StatusEffectSafeTemporaryTrait>(Name, subscribe: status => 
    status.trait = AbsentUtils.GetTrait("Zoomlin"))
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}