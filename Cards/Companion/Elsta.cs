using AbsentAvalanche.Traits;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Cards.Companion;

internal class Elsta() : AbstractCompanion(
    Name, "Elsta",
    5, 1, 4,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack("On Kill Apply Gold To Self", 4),
            AbsentUtils.SStack("MultiHit")
        ];
        card.traits =
        [
            AbsentUtils.TStack(GoldRush.Name)
        ];
    })
{
    public const string Name = "Elsta";

    public override CardDataBuilder Builder()
    {
        return base.Builder()
            .WithIdleAnimationProfile("Heartbeat2AnimationProfile");
    }
}