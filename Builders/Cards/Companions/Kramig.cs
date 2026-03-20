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
public class Kramig : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Kramig")
            .SetStats(8, 3, 4)
            .SetSprites(
                Absent.GetSprite("Kramig"),
                Absent.GetSprite("KramigBG"))
            .WithFlavour("Protects its friends")
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects = [Absent.SStack(DealAdditionalDamageForEachDamagedAlly.Name, 2)];
                card.greetMessages =
                [
                    "In the wild, an adult panda eats about 83 pounds of bamboo – every day! But this black and white softie doesn’t need any food, just a lot of love.",
                    "Protects its friends"
                ];
            });
    }
}