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
public class Bam : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "A friend for a home";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Bam",
                "TargetModeBasic",
                "Blood Profile Normal",
                "SwayAnimationProfile")
            .SetStats(3, 1, 8)
            .SetSprites(
                Absent.GetSprite("Bam"),
                Absent.GetSprite("BamBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(WhenAllyHitGainFrenzy.Name)
                ];
                card.greetMessages =
                [
                    "I lost my friend, have you seen her? She's a bamboo"
                ];
            });
    }
}