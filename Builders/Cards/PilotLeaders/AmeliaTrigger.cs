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
public class AmeliaTrigger : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Amelia")
            .SetStats(7, null, 7)
            .SetSprites(
                Absent.GetSprite("AmeliaTrigger"),
                Absent.GetSprite("AmeliaTriggerBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects = [
                    Absent.SStack(Flight.Name, 3),
                    Absent.SStack(WhenAllyRecalledItTriggersAgainstRandomEnemy.Name),
                    Absent.SStack(OnCardPlayedRecallAllAlliedCompanions.Name),
                ];
                card.traits =
                [
                    Absent.TStack(Pilot.Name)
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Brings you where you're meant to be";

    public bool LeaderExclusive => true;

    public bool InPool => true;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-1, 2),
        counterRange = new Vector2Int(-1, 1),
    };
}