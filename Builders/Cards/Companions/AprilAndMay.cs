#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class AprilAndMay : ILeaderBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Chaotic yet cuddly friends";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Sheep and Dino")
            .SetStats(8, 0, 3)
            .SetSprites(
                Absent.GetSprite("AprilAndMay"),
                Absent.GetSprite("AprilAndMayBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(DreamTeam.NameWhenDeployed(April.Name, May.Name))
                ];
                card.charmSlots *= 2;
            })
            .WithText("Share Mays bonus health");
    }
    
    public bool LeaderExclusive => false;

    public bool InPool => false;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new();
}