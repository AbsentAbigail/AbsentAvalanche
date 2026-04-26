#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

#endregion

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class Coral : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Coral",
                "TargetModeBasic",
                "Blood Profile Pink Wisp")
            .SetStats(10, 2, 4)
            .SetSprites(
                Absent.GetSprite("Coral"),
                Absent.GetSprite("CoralBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects = [
                    Absent.SStack(WhileActiveAlliesRetainCalm.Name),
                ];
                card.greetMessages =
                [
                    "Rosahaj!"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public static string Flavour = $"<color=#{KeywordColours.Pink.ToHexRGB()}>Loves and accepts you <3";

    public bool LeaderExclusive => false;

    public bool InPool => true;
    
    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-2, 2),
        damageRange = new Vector2Int(-1, 1),
        counterRange =  new Vector2Int(-1, 0),
    };
}