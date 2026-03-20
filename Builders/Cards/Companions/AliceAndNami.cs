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
public class AliceAndNami : ILeaderBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "When you are this cozy, no problem can stand in your way";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "The coziest pacas")
            .SetStats(10, 5, 4)
            .SetSprites(
                Absent.GetSprite("AliceAndNami"),
                Absent.GetSprite("AliceAndNamiBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(DreamTeam.NameWhenDeployed(Alice.Name, Nami.Name)),
                    Absent.SStack(WhenDeployedApplyRandomBuffToAllAllies.Name)
                ];
                card.charmSlots *= 2;
            });
    }
    
    public bool LeaderExclusive => false;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new();

    public bool InPool => false;
}