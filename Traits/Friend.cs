using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Traits;

internal class Friend() : AbstractTrait(Name, Keywords.Friend.Name,
    WhenAllyGainsNegativeStatusApplyToSelfInstead.Name, WhenAnAllyGainsAPositiveStatusShareHalfToSelf.Name)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}