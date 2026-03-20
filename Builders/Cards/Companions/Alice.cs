#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

#endregion

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class Alice : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Alice",
                "TargetModeBasic",
                "Blood Profile Snow",
                "SwayAnimationProfile")
            .SetStats(3, 2, 5)
            .SetSprites(
                Absent.GetSprite("Alice"),
                Absent.GetSprite("AliceBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(WhenEnemyIsKilledGainBlock.Name),
                    Absent.SStack(WhileActiveGainFrenzyEqualToBlock.Name)
                ];
                card.greetMessages =
                [
                    "*She moves her head forward expecting headpats*"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "*Cozy paca noises*";

    public bool LeaderExclusive => false;

    public bool InPool => true;
    
    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-2, 1),
        damageRange = new Vector2Int(-1, 2),
        counterRange =  new Vector2Int(-2, 0),
    };
}