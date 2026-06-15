using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class AmeliaSally : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Small bears on big adventures";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Amelia and Sally")
            .SetStats(7, 0, 3)
            .SetSprites(
                Absent.GetSprite("AmeliaSally"),
                Absent.GetSprite("AmeliaSallyBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(WhenCompanionKilledInsteadRecallThem.Name),
                ];
                card.createScripts =
                [
                    LeaderHelper.GiveUpgrade()
                ];
                card.charmSlots *= 2;
            })
            .WithCardType("Leader");
    }
}