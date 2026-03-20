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
public class LilGuy : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Lil Guy",
                "TargetModeBasic",
                "Blood Profile Snow",
                "SwayAnimationProfile")
            .SetStats(4, 2, 4)
            .SetSprites(
                Absent.GetSprite("LilGuy"),
                Absent.GetSprite("LilGuyBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(FakeCalm.Name, 3)
                ];
                card.traits =
                [
                    Absent.TStack(Scavenge.Name)
                ];
                card.createScripts =
                [
                    .. card.createScripts,
                    new Script<CardScriptGiveUpgrade>(
                        "Add Chuckle Charm",
                        script => script.upgradeData = Absent.GetCardUpgrade("CardUpgradeRemoveCharmLimit")
                    )
                ];
                card.charmSlots = int.MaxValue - 100_001;
                card.greetMessages =
                [
                    "Can I join on your adventure?"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Adventure time!";

    public bool LeaderExclusive => false;

    public bool InPool => true;
    
    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-1, 3),
        damageRange = new Vector2Int(-1, 2),
        counterRange =  new Vector2Int(-1, 0),
    };
}