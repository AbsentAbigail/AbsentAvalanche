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
public class CatcittenExplorer : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Catcitten",
                "TargetModeBasic",
                "Blood Profile Pink Wisp")
            .SetStats(5, 1, 4)
            .SetSprites(
                Absent.GetSprite("CatcittenExplorer"),
                Absent.GetSprite("CatcittenExplorerBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(ExplorerHaveShell.Name, 7),
                    Absent.SStack(WhenShellReachesAmountProgressExplorer.Name, 7),
                    Absent.SStack(ExplorerHaveSpice.Name, 7),
                    Absent.SStack(WhenSpiceReachesAmountProgressExplorer.Name, 7),
                    Absent.SStack(ExplorerHaveSnow.Name, 7),
                    Absent.SStack(WhenSnowReachesAmountProgressExplorer.Name, 7),
                    Absent.SStack(ExplorerHaveFrenzy.Name, 4),
                    Absent.SStack(WhenFrenzyReachesAmountProgressExplorer.Name, 3),
                ];
                card.greetMessages =
                [
                    "I'm weady to see da wowld!"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Their first big adventure!";

    public bool LeaderExclusive => false;

    public bool InPool => true;
    
    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-1, 2),
        damageRange = new Vector2Int(-1, 2),
        counterRange =  new Vector2Int(-1, 0),
    };
}