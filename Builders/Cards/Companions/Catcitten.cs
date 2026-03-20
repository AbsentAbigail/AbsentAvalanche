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
public class Catcitten : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Takes after their big sister, but still a bit shy!";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Catcitten",
                "TargetModeBasic",
                "Blood Profile Pink Wisp")
            .SetStats(3, 0, 5)
            .SetSprites(
                Absent.GetSprite("Catcitten"),
                Absent.GetSprite("CatcittenBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(Cat.Name),
                    Absent.SStack(WhenEnemyIsKilledApplyTeethToAttacker.Name),
                    Absent.SStack(WhenEnemyIsKilledCountDownAttacker.Name),
                ];
                card.greetMessages =
                [
                    "I got sepewated fwom my big sister, have you seen her? She's de coolest!"
                ];
            });
    }
}