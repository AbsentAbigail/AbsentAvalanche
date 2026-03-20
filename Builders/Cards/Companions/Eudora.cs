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
public class Eudora : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Eudora",
                idleAnim: "Heartbeat2AnimationProfile")
            .SetStats(4, 1, 8)
            .SetSprites(
                Absent.GetSprite("Eudora"),
                Absent.GetSprite("EudoraBG"))
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("MultiHit", 2),
                    Absent.SStack("Scrap", 2),
                    Absent.SStack(TriggerWhenAllyBehindTriggers.Name)
                ];
                card.greetMessages =
                [
                    "I'm all wound up!"
                ];
            });
    }
}