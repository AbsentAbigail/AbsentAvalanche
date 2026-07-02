using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class IceLanternCosplay : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Ice Lantern Cosplay")
            .SetDamage(null)
            .SetSprites(
                Absent.GetSprite("IceLanternCosplay"),
                Absent.GetSprite("IceLanternCosplayBG"))
            .WithValue(50)
            .CanPlayOnHand(false)
            .WithPools(CardPools.GeneralItems)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(Equip.Name),
                    Absent.SStack(WhenEquippedGainScrap.Name),
                    Absent.SStack("While Active Increase Attack To Allies")
                ];
            });
    }
}