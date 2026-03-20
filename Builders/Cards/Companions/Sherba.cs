#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Builders.Traits;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

#endregion

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class Sherba : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Sherba",
                bloodProfile: "Blood Profile Snow")
            .SetStats(6, 0, 6)
            .SetSprites(
                Absent.GetSprite("Sherba"),
                Absent.GetSprite("SherbaBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Absent.SStack("Snow")
                ];
                card.startWithEffects =
                [
                    Absent.SStack(WhenAllyHitIncreaseEffects.Name)
                ];
                card.traits =
                [
                    Absent.TStack("Barrage"),
                    Absent.TStack(Warm.Name)
                ];
                card.greetMessages =
                [
                    "Wants cozy cuddles"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Warm and cozy";

    public bool LeaderExclusive => false;

    public bool InPool => true;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-2, 3),
        counterRange = new Vector2Int(-2, 1)
    };
}