using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class PlushBag : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "So much joy fits into one backpack";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Plush Bag")
            .WithFlavour(Flavour)
            .SetDamage(null)
            .NeedsTarget(false)
            .SetSprites(
                Absent.GetSprite("PlushBag"),
                Absent.GetSprite("PlushBagBG"))
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.createScripts = [
                    LeaderHelper.GiveUpgrade()
                ];
                card.startWithEffects =
                [
                    Absent.SStack(OnCardPlayedAddRandomPlushToHand.Name, 3)
                ];
                card.traits =
                [
                    Absent.TStack("Consume")
                ];
            });
    }
}