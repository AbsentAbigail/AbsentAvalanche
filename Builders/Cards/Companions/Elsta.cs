#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Traits;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class Elsta : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Ooo shiny!";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Elsta",
                idleAnim: "Heartbeat2AnimationProfile")
            .SetStats(5, 1, 4)
            .SetSprites(
                Absent.GetSprite("Elsta"),
                Absent.GetSprite("ElstaBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("On Kill Apply Gold To Self", 4),
                    Absent.SStack("MultiHit")
                ];
                card.traits =
                [
                    Absent.TStack(GoldRush.Name)
                ];
                card.greetMessages =
                [
                    "*stares at your Bling pouch*"
                ];
            });
    }
}