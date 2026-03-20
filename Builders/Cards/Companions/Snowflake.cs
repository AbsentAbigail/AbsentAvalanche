#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

#endregion

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class Snowflake : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Snowflake",
                bloodProfile: "Blood Profile Snow")
            .SetStats(9, 3, 5)
            .SetSprites(
                Absent.GetSprite("Snowflake"),
                Absent.GetSprite("SnowflakeBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("When Hit Apply Snow To Attacker", 3),
                ];
                card.traits = [
                    Absent.TStack("Heartburn"),
                ];
                card.greetMessages =
                [
                    "Hi, I'm Snowflake. I like warm hugs"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Lost and found, now warm and cozy";

    public bool LeaderExclusive => false;

    public bool InPool => true;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-1, 2),
        damageRange = new Vector2Int(-1, 1),
        counterRange = new Vector2Int(-1, 1),
    };
}