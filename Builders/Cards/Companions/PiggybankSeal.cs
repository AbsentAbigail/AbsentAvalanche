using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class PiggybankSeal : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Piggybank Seal")
            .SetStats(5, 0, 5)
            .SetSprites(
                Absent.GetSprite("PiggybankSeal"),
                Absent.GetSprite("PiggybankSealBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("When Hit Apply Gold To Attacker (No Ping)", 5),
                ];
                card.traits =
                [
                    Absent.TStack("Greed"),
                ];
                card.greetMessages =
                [
                    "Do you wanna save some money?",
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Give him your money, he'll take good care of it";

    public bool LeaderExclusive => false;

    public bool InPool => true;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-2, 1),
        counterRange = new Vector2Int(-1, 0),
    };
}