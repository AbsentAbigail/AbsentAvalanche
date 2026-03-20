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
public class May : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "May",
                "TargetModeBasic",
                "Blood Profile Pink Wisp",
                "GiantAnimationProfile")
            .SetStats(4, null, 3)
            .SetSprites(
                Absent.GetSprite("May"),
                Absent.GetSprite("MayBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("Teeth", 2),
                    Absent.SStack("On Turn Apply Teeth To Self"),
                    Absent.SStack(OnKillIncreaseHealthPermanent.Name)
                ];
                card.greetMessages =
                [
                    "Gawr! I'm a big scawy dinosauw!"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Very cuddly when she's not busy ruining a toy city";

    public bool LeaderExclusive => false;

    public bool InPool => true;
    
    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-1, 1),
        counterRange =  new Vector2Int(-1, 1),
    };
}