using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Cards.Companion
{
    internal class Elsta() : AbstractUnit(
        Name, "Elsta",
        5, 1, 4,
        subscribe: card =>
        {
            card.startWithEffects = [
                AbsentUtils.SStack("On Kill Apply Gold To Self", 4),
                AbsentUtils.SStack("MultiHit", 1),
            ];
            card.traits = [
                AbsentUtils.TStack(Traits.GoldRush.Name),
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
}