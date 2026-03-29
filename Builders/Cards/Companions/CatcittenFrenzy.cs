#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class CatcittenFrenzy : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Her first big adventure!";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Catcitten",
                "TargetModeBasic",
                "Blood Profile Pink Wisp")
            .SetStats(5, 1, 4)
            .SetSprites(
                Absent.GetSprite("CatcittenFrenzy"),
                Absent.GetSprite("CatcittenFrenzyBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("MultiHit"),
                    Absent.SStack("While Active Frenzy To Allies")
                ];
            });
    }
}