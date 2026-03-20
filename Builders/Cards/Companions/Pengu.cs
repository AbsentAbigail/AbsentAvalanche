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
public class Pengu : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Pengu",
                bloodProfile: "Blood Profile Snow")
            .SetStats(5, 0, 5)
            .SetSprites(
                Absent.GetSprite("Pengu"),
                Absent.GetSprite("PenguBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(WhenSnowedGainMultiHit.Name)
                ];
                card.createScripts =
                [
                    .. card.createScripts,
                    new Script<CardScriptGiveUpgrade>(
                        "Add Pengu Charm",
                        script => script.upgradeData = Absent.GetCardUpgrade("CardUpgradeSnowImmune")
                    )
                ];
                card.charmSlots = 4;
                card.greetMessages =
                [
                    "When you need a rest, I can be your pillow!"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Pillow Pet!";

    public bool LeaderExclusive => false;

    public bool InPool => true;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-1, 1),
        damageRange = new Vector2Int(0, 1)
    };
}