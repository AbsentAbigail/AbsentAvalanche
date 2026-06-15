using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Builders.Traits;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace AbsentAvalanche.Builders.Cards.PilotLeaders;

[UsedImplicitly]
public class AmeliaFreePlay : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Amelia")
            .SetStats(6, 3, 5)
            .SetSprites(
                Absent.GetSprite("AmeliaFreePlay"),
                Absent.GetSprite("AmeliaFreePlayBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects = [
                    Absent.SStack(Flight.Name, 3),
                    Absent.SStack(WhenAllyRecalledGainFreePlay.Name),
                ];
                card.traits =
                [
                    Absent.TStack(Pilot.Name)
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Pet friendly airline";

    public bool LeaderExclusive => true;

    public bool InPool => true;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-1, 2),
        counterRange = new Vector2Int(-1, 1),
    };
}