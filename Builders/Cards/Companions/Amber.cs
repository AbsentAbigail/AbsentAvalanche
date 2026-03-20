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
public class Amber : ILeaderBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Her favourite spot is on the radiator";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Amber",
                "TargetModeBasic",
                "Blood Profile Berry",
                "SwayAnimationProfile")
            .SetStats(9, null, 4)
            .SetSprites(
                Absent.GetSprite("Amber"),
                Absent.GetSprite("AmberBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(OnCardPlayedGainHeating.Name, 2),
                ];
                card.traits =
                [
                    Absent.TStack(Heating.Name),
                    Absent.TStack(Warm.Name),
                ];
                card.greetMessages =
                [
                    "I can keep your team warm"
                ];
            });
    }
    
    public bool LeaderExclusive => false;

    public bool InPool => true;
    
    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(0, 3),
        counterRange =  new Vector2Int(-1, 0),
    };
}