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
public class CatcusZoom : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Catcus",
                "TargetModeBasic",
                "Blood Profile Pink Wisp")
            .SetStats(6, 0, 10)
            .SetSprites(
                Absent.GetSprite("CatcusZoom"),
                Absent.GetSprite("CatcusZoomBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(Cat.Name, 3),
                    Absent.SStack(OnBattleWonDecreaseCounter.Name),
                    Absent.SStack(OnBattleWonGainFrenzy.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Cat gets zoomies";

    public bool LeaderExclusive => false;

    public bool InPool => true;
    
    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(0, 2),
        counterRange =  new Vector2Int(0, 1),
    };
}