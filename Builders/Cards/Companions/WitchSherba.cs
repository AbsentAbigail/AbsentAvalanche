using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Builders.Traits;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class WitchSherba : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Witch Sherba",
                bloodProfile: "Blood Profile Snow")
            .SetStats(6, 0, 3)
            .SetSprites(
                Absent.GetSprite("WitchSherba"),
                Absent.GetSprite("WitchSherbaBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Absent.SStack("Overload")
                ];
                card.startWithEffects =
                [
                    Absent.SStack(WhileActiveHasFrenzyEqualToCurrentCounter.Name)
                ];
                card.traits =
                [
                    Absent.TStack("Smackback")
                ];
                card.greetMessages =
                [
                    "Hehehee witchy hours are here"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Warm and cozy";

    public bool LeaderExclusive => false;

    public bool InPool => true;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-1, 2),
        counterRange = new Vector2Int(-1, 1)
    };
}