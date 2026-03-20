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
public class PanickedNut : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Panicked Nut",
                bloodProfile: "Blood Profile Husk",
                idleAnim: "ShakeAnimationProfile")
            .SetStats(3, 1, 3)
            .SetSprites(
                Absent.GetSprite("PanickedNut"),
                Absent.GetSprite("PanickedNutBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(WhenDeployedGainShellForEachEnemy.Name, 3),
                    Absent.SStack(WhenDeployedGainSnowForEachEnemy.Name),
                    Absent.SStack("Teeth")
                ];
                card.greetMessages =
                [
                    "Please don't make me fight..."
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Won't leave its shell";
}