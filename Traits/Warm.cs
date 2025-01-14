using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Traits;

public class Warm() : AbstractTrait(Name, Keywords.Warm.Name, EveryTurnCountDownSnowFrostBlock.Name)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}