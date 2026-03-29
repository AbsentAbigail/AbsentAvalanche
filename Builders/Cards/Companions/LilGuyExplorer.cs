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
public class LilGuyExplorer : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Lil Guy",
                "TargetModeBasic",
                "Blood Profile Snow",
                "SwayAnimationProfile")
            .SetStats(5, 2, 4)
            .SetSprites(
                Absent.GetSprite("LilGuy1"),
                Absent.GetSprite("LilGuy1BG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("On Turn Apply Shell To Self"),
                    Absent.SStack(ExplorerDefeatBossFight.Name),
                    Absent.SStack(WhenBossDefeatedProgressExplorer.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Exploring and making friends!";

    public bool LeaderExclusive => true;

    public bool InPool => true;
    
    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-1, 1),
        damageRange = new Vector2Int(-1, 1),
        counterRange =  new Vector2Int(-1, 1),
    };
}