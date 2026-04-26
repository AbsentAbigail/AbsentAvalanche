#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Traits;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class PhotoCollection : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Photo Collection")
            .SetDamage(3)
            .SetSprites(Absent.GetSprite("PhotoCollection"), Absent.GetSprite("PhotoCollectionBG"))
            .WithPools(CardPools.GeneralItems)
            .CanPlayOnHand(false)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.traits = [
                    Absent.TStack(Scavenge.Name, 2)
                ];
                card.createScripts =
                [
                    .. card.createScripts,
                    new Script<CardScriptGiveUpgrade>(
                        "Add Chuckle Charm",
                        script => script.upgradeData = Absent.GetCardUpgrade("CardUpgradeRemoveCharmLimit")
                    )
                ];
                card.charmSlots = int.MaxValue - 100_001;
            });
    }
}