using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class PumpkinOctavia : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Pumpkin Octavia")
            .SetStats(7, 1, 6)
            .SetSprites(
                Absent.GetSprite("PumpkinOctavia"),
                Absent.GetSprite("PumpkinOctaviaBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("Shell"),
                    Absent.SStack(OnCardPlayedIncreaseStatusesOfEveryone.Name),
                ];
                card.greetMessages =
                [
                    "Hey! Let's carve some pumpkins c:<",
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Even octopi get into the Halloween spirit";

    public bool LeaderExclusive => false;

    public bool InPool => true;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-1, 1),
        damageRange = new Vector2Int(0, 1),
        counterRange = new Vector2Int(-1, 0),
    };
}