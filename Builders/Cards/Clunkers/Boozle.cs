#region

using AbsentAvalanche.Builders.Interfaces;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Clunkers;

[UsedImplicitly]
public class Boozle : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "A home for a friend";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Boozle")
            .WithCardType("Clunker")
            .SetHealth(null)
            .SetDamage(null)
            .SetSprites(Absent.GetSprite("Boozle"), Absent.GetSprite("BoozleBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralItems)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("Scrap", 3),
                    Absent.SStack("When Hit Apply Shell To Self", 3)
                ];
            });
    }
}