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
public class SlothPillow : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Sloth Pillow",
                "TargetModeBasic",
                "Blood Profile Berry",
                "FloatSquishAnimationProfile")
            .SetStats(12, 0, 4)
            .SetSprites(
                Absent.GetSprite("SlothPillow"),
                Absent.GetSprite("SlothPillowBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Absent.SStack(Kill.Name),
                ];
                card.startWithEffects =
                [
                    Absent.SStack(WhenRedrawHitTriggerEnemies.Name)
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Sleepy eepy";

    public bool LeaderExclusive => true;

    public bool InPool => true;
    
    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(0, 3),
        counterRange =  new Vector2Int(-1, 1),
    };
}