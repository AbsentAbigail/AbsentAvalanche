using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class Airship : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Airship")
            .SetStats(8)
            .SetSprites(
                Absent.GetSprite("Airship"),
                Absent.GetSprite("AirshipBG"))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(WhenDeployedDrawAlliesAndApplyZoomlin.Name)
                ];
                card.traits =
                [
                    Absent.TStack("Noomlin")
                ];
            });
    }
}