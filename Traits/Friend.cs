using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Traits;

internal class Friend() : AbstractTrait(Name, Keywords.Friend.Name,
    WhenAllyGainsNegativeStatusApplyToSelfInstead.Name, WhenAnAllyGainsAPositiveStatusShareHalfToSelf.Name)
{
    public const string Name = "Friend";
}