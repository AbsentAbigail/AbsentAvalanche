using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

internal class Aftonsparv() : AbstractCompanion(
    Name, "Aftonsparv",
    10, null, 3,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack("On Card Played Apply Frost To RandomEnemy"),
            AbsentUtils.SStack(OnTurnSummonUfoInHand.Name)
        ];
        card.greetMessages =
        [
            "A soft alien is the best buddy to bring on an imaginary flight in space. What will your child get up to this time with their superhero?",
            "Gnarp Gnarp from space"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Alien Friend";
    protected override string IdleAnimation => "FloatAnimationProfile";
    protected override string BloodProfile => "Blood Profile Pink Wisp";
}