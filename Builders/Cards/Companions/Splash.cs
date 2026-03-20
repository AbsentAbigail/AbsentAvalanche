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
public class Splash : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Splash",
                bloodProfile: "Blood Profile Black",
                idleAnim: "FloatAnimationProfile")
            .SetStats(6, 0, 3)
            .SetSprites(
                Absent.GetSprite("Splash"),
                Absent.GetSprite("SplashBG"))
            .WithFlavour("Won't leave its shell")
            .WithPools(CardPools.ShademancerUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects = [
                    Absent.SStack("Overload"),
                ];
                card.startWithEffects =
                [
                    Absent.SStack(WhenDeployedApplyOverburnToEnemies.Name, 2),
                ];
                card.greetMessages =
                [
                    "I'm coming in hot",
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}