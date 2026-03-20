#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Traits;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class BlingThrow : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Bling Throw")
            .SetDamage(5)
            .SetSprites(Absent.GetSprite("BlingThrow"), Absent.GetSprite("BlingThrowBG"))
            .WithPools(CardPools.GeneralItems)
            .CanPlayOnHand(false)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.traits = [Absent.TStack(GoldRush.Name)];
                card.startWithEffects =
                [
                    Absent.SStack("Pre Turn Take Gold", 2),
                    Absent.SStack("On Kill Apply Gold To Self", 2)
                ];
            });
    }
}