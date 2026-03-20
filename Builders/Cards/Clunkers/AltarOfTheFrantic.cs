#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Clunkers;

[UsedImplicitly]
public class AltarOfTheFrantic : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Altar of the Frantic", idleAnim: "PulseAnimationProfile")
            .WithCardType("Clunker")
            .SetHealth(null)
            .SetDamage(null)
            .SetSprites(Absent.GetSprite("AltarOfTheFrantic"), Absent.GetSprite("AltarOfTheFranticBG"))
            .WithPools(CardPools.GeneralItems)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("Scrap"),
                    Absent.SStack(WhileActiveAddFrenzyToItemsInHand.Name),
                    Absent.SStack(WhileActiveAddAimlessToItemsInHand.Name),
                ];
            });
    }
}