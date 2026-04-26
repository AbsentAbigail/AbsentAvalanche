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
public class Tiramisu : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Tiramisu")
            .SetStats(13, 0, 4)
            .SetSprites(
                Absent.GetSprite("Tiramisu"),
                Absent.GetSprite("TiramisuBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Absent.SStack(InstantApplyBomEqualToApplierBom.Name)
                ];
                card.startWithEffects =
                [
                    Absent.SStack("Weakness"),
                    Absent.SStack(OnCardPlayedGainBom.Name),
                ];
                card.traits =
                [
                    Absent.TStack("Barrage")
                ];
                card.greetMessages =
                [
                    "Hi, I'm Tiramisu!"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Curious little buddy";

    public bool LeaderExclusive => false;

    public bool InPool => true;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-1, 2),
        damageRange = new Vector2Int(0, 2),
        counterRange = new Vector2Int(-1, 1),
    };
}