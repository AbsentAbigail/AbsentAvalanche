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
public class Blackfisk : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Bläckfisk",
                "TargetModeBasic",
                "Blood Profile Blue (x2)",
                "PulseAnimationProfile")
            .SetStats(8, 0, 8)
            .SetSprites(
                Absent.GetSprite("Blackfisk"),
                Absent.GetSprite("BlackfiskBG"))
            .WithFlavour("8 arms to give 8 times better hugs!")
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects = [Absent.SStack(InstantIncreaseCurrentCounter.Name)];
                card.startWithEffects = [Absent.SStack("MultiHit", 7)];
                card.traits =
                [
                    Absent.TStack("Pull"),
                    Absent.TStack("Aimless")
                ];
                card.greetMessages =
                [
                    "The octopus is a truly unique marine animal with its 8 arms and the ability to camouflage itself. Imagine all the exciting adventures your child can experience with such a companion by their side.",
                    "8 arms to give 8 times better hugs!"
                ];
            });
    }
}