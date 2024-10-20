using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Traits;

internal class GoldRush() : AbstractTrait(Name, Keywords.GoldRush.Name, GoldRushEffect.Name)
{
    public const string Name = "GoldRush";
}