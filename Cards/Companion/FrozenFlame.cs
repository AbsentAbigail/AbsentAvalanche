using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Companion
{
    internal class FrozenFlame() : AbstractUnit(
        Name, "Frozen Flame",
        1, null, 1,
        Pools.Shademancer,
        card =>
        {
            card.startWithEffects = [
                AbsentUtils.SStack(OnCardPlayedGainOverload.Name, 1),
                AbsentUtils.SStack("Block", 4),
                AbsentUtils.SStack(WhenDestroyedSummonUnboundFlame.Name),
            ];
        })
    {
        public const string Name = "FrozenFlame";
    }
}