using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class VeiledLadyCosplay : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Veiled Lady Cosplay")
            .SetDamage(0)
            .SetHealth(2)
            .SetSprites(
                Absent.GetSprite("VeiledLadyCosplay"),
                Absent.GetSprite("VeiledLadyCosplayBG"))
            .WithValue(50)
            .CanPlayOnHand(false)
            .WithPools(CardPools.GeneralItems)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Absent.SStack("Shroom", 3)
                ];
                card.startWithEffects =
                [
                    Absent.SStack(Equip.Name)
                ];
            });
    }
}