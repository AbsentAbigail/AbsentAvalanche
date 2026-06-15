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
public class AmeliaFrenzy : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Amelia")
            .SetStats(5, 1, 5)
            .SetSprites(
                Absent.GetSprite("AmeliaFrenzy"),
                Absent.GetSprite("AmeliaFrenzyBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects = [
                    Absent.SStack(Flight.Name, 3),
                    Absent.SStack(OnCardPlayedRecallAllyBehindAndApplyFrenzy.Name),
                ];
                card.traits =
                [
                    Absent.TStack(Pilot.Name)
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Lifts you up";

    public bool LeaderExclusive => true;

    public bool InPool => true;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-1, 2),
        damageRange = new Vector2Int(0, 1),
        counterRange = new Vector2Int(-1, 1),
    };
}