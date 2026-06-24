using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Builders.Traits;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class GrinkNova : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Grink Nova",
                bloodProfile: "Blood Profile Snow")
            .SetStats(8, 2, 4)
            .SetSprites(
                Absent.GetSprite("GrinkNova"),
                Absent.GetSprite("GrinkNovaBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(Flight.Name),
                    Absent.SStack(WhenAllyHitApplyFrostToAttacker.Name, 2),
                    Absent.SStack(OnCardPlayedDealDamageToAllFrostedEnemiesEqualToFrost.Name),
                ];
                card.greetMessages =
                [
                    "Do you like my Grink costume? <3"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "That's not a reindeer, what are you talking about?";

    public bool LeaderExclusive => false;

    public bool InPool => true;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-2, 1),
        counterRange = new Vector2Int(-1, 0)
    };
}