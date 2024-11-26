using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Clunkers;

public class PillowFortress() : AbstractClunker(
    Name, "Pillow Fortress",
    2, pools: Pools.None,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            ..card.startWithEffects,
            AbsentUtils.SStack(WhenHitSummonPillow.Name)
        ];
    })
{
    public const string Name = "PillowFortress";
    public override string FlavourText => "A stronghold to ward of evil, or to sleep in";
    protected override string IdleAnimation => "PulseAnimationProfile";
}