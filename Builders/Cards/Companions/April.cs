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
public class April : ILeaderBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Woolly Friend";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "April",
                "TargetModeBasic",
                "Blood Profile Normal",
                "SwayAnimationProfile")
            .SetStats(4, 0, 4)
            .SetSprites(
                Absent.GetSprite("April"),
                Absent.GetSprite("AprilBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(OnCardPlayedAddWoolGrenadeToHand.Name)
                ];
                card.greetMessages =
                [
                    "Is it time to throw soft explosives?"
                ];
            });
    }
    
    public bool LeaderExclusive => false;

    public bool InPool => true;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(2, 3),
        counterRange =  new Vector2Int(-1, 0),
        subscribe = card =>
        {
            card.traits =
            [
                .. card.traits,
                Absent.TStack("Spark")
            ];
            card.startWithEffects =
            [
                .. card.startWithEffects,
                Absent.SStack(WhileActiveCountDownEtherealWhenDrawn.Name)
            ];
        }
    };
}