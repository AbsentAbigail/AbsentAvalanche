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
public class SkeletonPatch : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Skeleton Patch")
            .SetStats(8, null, 4)
            .SetSprites(
                Absent.GetSprite("SkeletonPatch"),
                Absent.GetSprite("SkeletonPatchBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("When Hit Gain Teeth To Self", 2),
                    Absent.SStack(OnCardPlayedHealEqualToTeeth.Name),
                ];
                card.greetMessages =
                [
                    "My friends helped me dig these bones up"
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Throw the dog a bone, would you?";

    public bool LeaderExclusive => false;

    public bool InPool => true;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new()
    {
        healthRange = new Vector2Int(-2, 2),
        counterRange = new Vector2Int(-1, 1)
    };
}