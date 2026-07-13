using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class GravestonePengu : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Gravestone Pengu",
                bloodProfile: "Blood Profile Fungus", idleAnim: "PulseAnimationProfile")
            .SetStats(9)
            .SetSprites(
                Absent.GetSprite("GravestonePengu"),
                Absent.GetSprite("GravestonePenguBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(ShroomDamagesAdditionalTimes.Name),
                    Absent.SStack(WhenHitApplyShroomToAllEnemies.Name),
                ];
                card.greetMessages =
                [
                    "Will you leave a flower here?"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "A memorial for all the lost plush friends";

    public bool LeaderExclusive => false;

    public bool InPool => true;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-1, 1),
        damageRange = new Vector2Int(0, 1)
    };
}