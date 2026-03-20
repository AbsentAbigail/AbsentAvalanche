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
public class Nova : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Nova",
                "TargetModeBasic",
                "Blood Profile Normal",
                "SwayAnimationProfile")
            .SetStats(5, 1, 2)
            .SetSprites(
                Absent.GetSprite("Nova"),
                Absent.GetSprite("NovaBG"))
            .WithFlavour("Likes playing in the snow")
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("When Snow Applied To Self Gain Equal Attack"),
                    Absent.SStack(HitAllSnowed.Name)
                ];
                card.greetMessages =
                [
                    "*She stares at you*"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Smol rhino friend with big love";

    public bool LeaderExclusive => false;

    public bool InPool => true;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-1, 1),
        damageRange = new Vector2Int(0, 1)
    };
}