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
public class Jerry : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Jerry",
                "TargetModeBasic",
                "Blood Profile Pink Wisp")
            .SetStats(5, 2, 5)
            .SetSprites(
                Absent.GetSprite("Jerry"),
                Absent.GetSprite("JerryBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects = [
                    Absent.SStack(FakeCalm.Name, 6),
                    Absent.SStack(WhenItemPlayedGiveItZoomlin.Name),
                ];
                card.greetMessages =
                [
                    "That's a lot of sharks"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Accepts and loves you <5";

    public bool LeaderExclusive => false;

    public bool InPool => true;
    
    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(1, 2),
        damageRange = new Vector2Int(-1, 2),
        counterRange =  new Vector2Int(-1, 0),
    };
}