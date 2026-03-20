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
public class SherbaAndCuddles : ILeaderBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "No place is better than the arms of your best friend";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Snuggle Buddies")
            .SetStats(26, 0, 6)
            .SetSprites(
                Absent.GetSprite("SherbaAndCuddles"),
                Absent.GetSprite("SherbaAndCuddlesBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(DreamTeam.NameWhenDeployed(Sherba.Name, Cuddles.Name)),
                    Absent.SStack(WhenDeployedApplySnowToEnemies.Name),
                ];
                card.charmSlots *= 2;
            });
    }
    
    public bool LeaderExclusive => false;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new();

    public bool InPool => false;
}