using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Traits;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class Spot2 : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Spot")
            .SetStats(8, 3, 5)
            .SetSprites(
                Absent.GetSprite("Spot2"),
                Absent.GetSprite("Spot2BG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.traits =
                [
                    Absent.TStack("Longshot"),
                    Absent.TStack("Pull"),
                    Absent.TStack(Friend.Name),
                ];
                card.greetMessages =
                [
                    "I wanna help protect the friends"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Always eager to give hugs";

    public bool LeaderExclusive => false;

    public bool InPool => true;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(0, 1),
        damageRange = new Vector2Int(0, 2),
        counterRange = new Vector2Int(-1, 0),
    };
}