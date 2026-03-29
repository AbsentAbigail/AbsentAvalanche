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
public class CatcittenSpice : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Her first big adventure!";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Catcitten",
                "TargetModeBasic",
                "Blood Profile Pink Wisp")
            .SetStats(5, 2, 4)
            .SetSprites(
                Absent.GetSprite("CatcittenSpice"),
                Absent.GetSprite("CatcittenSpiceBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("Spice", 6),
                    Absent.SStack(WhenAllyHitGainEqualSpice.Name, 4)
                ];
            });
    }
}