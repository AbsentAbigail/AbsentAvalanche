using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffects;

namespace AbsentAvalanche.Cards.Companion
{
    internal class FrozenFlame() : AbstractUnit(
        Name, "Frozen Flame",
        1, null, 1,
        Pools.Shademancer,
        card =>
        {
            card.startWithEffects = [
                Absent.SStack(OnCardPlayedGainOverload.Name, 1),
                Absent.SStack("Block", 4),
            ];
        })
    {
        public const string Name = "FrozenFlame";
    }
}