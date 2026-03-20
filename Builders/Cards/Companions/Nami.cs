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
public class Nami : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Nami",
                "TargetModeBasic",
                "Blood Profile Pink Wisp")
            .SetStats(7, 2, 4)
            .SetSprites(
                Absent.GetSprite("Nami"),
                Absent.GetSprite("NamiBG"))
            .WithFlavour("*Rainbow paca noises*")
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(OnCardPlayedApplyRandomBuffToRandomAlly.Name)
                ];
                card.greetMessages =
                [
                    "*She starts cuddling you*"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "*Rainbow paca noises*";

    public bool LeaderExclusive => false;

    public bool InPool => true;
    
    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(0, 1),
        damageRange = new Vector2Int(0, 2),
        counterRange =  new Vector2Int(-1, 0),
    };
}