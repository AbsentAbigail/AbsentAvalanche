#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class Aftonsparv : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Aftonsparv",
                "TargetModeBasic",
                "Blood Profile Pink Wisp",
                "FloatAnimationProfile")
            .SetStats(10, null, 3)
            .SetSprites(
                Absent.GetSprite("Aftonsparv"),
                Absent.GetSprite("AftonsparvBG"))
            .WithFlavour("Alien Friend")
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("On Card Played Apply Frost To RandomEnemy"),
                    Absent.SStack(OnTurnSummonUfoInHand.Name)
                ];
                card.greetMessages =
                [
                    "A soft alien is the best buddy to bring on an imaginary flight in space. What will your child get up to this time with their superhero?",
                    "Gnarp Gnarp from space"
                ];
            });
    }
}