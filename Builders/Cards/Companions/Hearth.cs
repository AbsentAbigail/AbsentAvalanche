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
public class Hearth : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Hearth",
                "TargetModeBasic",
                "Blood Profile Snow")
            .SetStats(9, 2, 5)
            .SetSprites(
                Absent.GetSprite("Hearth"),
                Absent.GetSprite("HearthBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(WhileActiveAlliesSnowFrostResist.Name),
                ];
                card.traits =
                [
                    Absent.TStack(Warm.Name)
                ];
                card.greetMessages =
                [
                    "Please put me in the microwave, I'm freezing"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Heated noodle puppy";

    public bool LeaderExclusive => false;

    public bool InPool => true;
    
    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(0, 1),
        damageRange = new Vector2Int(0, 1),
        counterRange =  new Vector2Int(-1, 0),
    };
}