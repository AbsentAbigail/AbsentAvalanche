using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Cards.Companion;

internal class Aftonsparv() : AbstractCompanion(
    Name, "Aftonsparv",
    10, null, 3,
    subscribe: card =>
    {
        card.startWithEffects = [
            AbsentUtils.SStack("On Card Played Apply Frost To RandomEnemy"),
            AbsentUtils.SStack(OnTurnSummonUFOInHand.Name)
        ];
        card.greetMessages =
        [
            "A soft alien is the best buddy to bring on an imaginary flight in space. What will your child get up to this time with their superhero?",
            "Gnarp Gnarp from space"
        ];
    })
{
    public const string Name = "Aftonsparv";

    public override CardDataBuilder Builder()
    {
        return base.Builder()
            .WithBloodProfile("Blood Profile Pink Wisp")
            .WithFlavour("Alien Friend")
            .WithIdleAnimationProfile("FloatAnimationProfile");
    }
}