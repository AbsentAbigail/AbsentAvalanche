#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class Pengulina : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Pengulina")
            .WithFlavour(Flavour)
            .SetDamage(0)
            .SetSprites(Absent.GetSprite("Pengulina"), Absent.GetSprite("PengulinaBG"))
            .WithPools(CardPools.GeneralItems)
            .CanPlayOnHand(false)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Absent.SStack("Snow"),
                    Absent.SStack("Frost"),
                ];
                card.startWithEffects =
                [
                    Absent.SStack("Hit All Enemies")
                ];
            });
    }

    public const string Flavour = """
                                  Pocket hug: Happy Penguin!
                                  I may be a small penguin, but I believe in you.
                                  You got this!
                                  """;
}