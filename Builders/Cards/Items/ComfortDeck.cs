using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class ComfortDeck : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Comfort Deck")
            .SetDamage(null)
            .NeedsTarget(false)
            .SetSprites(
                Absent.GetSprite("ComfortDeck"),
                Absent.GetSprite("ComfortDeckBG"))
            .WithPools(CardPools.GeneralItems)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(OnCardPlayedShuffleDeck.Name)
                ];
                card.traits =
                [
                    Absent.TStack("Draw", 2),
                    Absent.TStack("Noomlin"),
                ];
            });
    }
}