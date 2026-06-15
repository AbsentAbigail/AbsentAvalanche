using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class Paperplane : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Paper plane")
            .SetStats(3, 3, 6)
            .SetSprites(
                Absent.GetSprite("Paperplane"),
                Absent.GetSprite("PaperplaneBG"))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(WhenDeployedGainFlight.Name),
                    Absent.SStack(WhenRecalledTrigger.Name),
                ];
            });
    }
}