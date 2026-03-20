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
public class Lusine : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Lusine",
                idleAnim: "PulseAnimationProfile")
            .SetStats(8, 2, 4)
            .SetSprites(
                Absent.GetSprite("Lusine"),
                Absent.GetSprite("LusineBG"))
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Absent.SStack("Demonize")
                ];
                card.startWithEffects =
                [
                    Absent.SStack("MultiHit")
                ];
                card.traits =
                [
                    Absent.TStack(Trample.Name)
                ];
                card.greetMessages =
                [
                    "Don't stand in my way, or I'll have to step on you"
                ];
            });
    }
}