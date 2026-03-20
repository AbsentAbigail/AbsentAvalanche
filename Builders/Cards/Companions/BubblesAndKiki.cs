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
public class BubblesAndKiki : ILeaderBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "True friends do everything together";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Fluff Friends")
            .SetStats(12, 1, 3)
            .SetSprites(
                Absent.GetSprite("BubblesAndKiki"),
                Absent.GetSprite("BubblesAndKikiBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(DreamTeam.NameWhenDeployed(Bubbles.Name, Kiki.Name)),
                    Absent.SStack(WhenDeployedChargeRedrawBell.Name),
                ];
                card.charmSlots *= 2;
            });
    }
    
    public bool LeaderExclusive => false;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new();

    public bool InPool => false;
}