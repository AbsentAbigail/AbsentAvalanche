using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Items;

public class Sarcophagus() : AbstractItem(Name, "Sarcophagus",
    pools: Pools.None, subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(Ethereal.Name, 3),
            AbsentUtils.SStack(WhenDestroyedSummonSarcophagus.Name, 2)
        ];
    })
{
    public const string Name = "Sarcophagus";
    protected override string IdleAnimation => "PulseAnimationProfile";
}