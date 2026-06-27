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
public class Doodle : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Doodle")
            .SetStats(3, 1, 6)
            .SetSprites(
                Absent.GetSprite("Doodle"),
                Absent.GetSprite("DoodleBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("Hit All Enemies")
                ];
                card.greetMessages =
                [
                    "°-°"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "pcrowDoodle";

    public bool LeaderExclusive => false;

    public bool InPool => true;
    
    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-1, 1),
        counterRange =  new Vector2Int(-1, 1),
    };
}