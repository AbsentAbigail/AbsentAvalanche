using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Traits;

public class Warm() : AbstractTrait(Name, Keywords.Warm.Name, EveryTurnCountDownSnowFrostBlock.Name)
{
    public const string Name = "Warm";
}