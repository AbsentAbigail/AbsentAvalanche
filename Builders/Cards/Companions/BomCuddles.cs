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
public class BomCuddles : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Bom Cuddles")
            .SetStats(12, 0, 4)
            .SetSprites(
                Absent.GetSprite("BomCuddles"),
                Absent.GetSprite("BomCuddlesBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(WhenHitApplyBomToRandomEnemyThreeTimes.Name, 2)
                ];
                card.traits =
                [
                    Absent.TStack("Barrage")
                ];
                card.greetMessages =
                [
                    "Do you wanna go trick or treating together?"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Bom shaped sweets for everyone!";

    public bool LeaderExclusive => false;

    public bool InPool => true;
    
    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-2, 2),
        counterRange =  new Vector2Int(-1, 1),
    };
}