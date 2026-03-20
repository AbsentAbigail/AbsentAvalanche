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
public class BubblesAndCuddles : ILeaderBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Bubbly feelings of cuddly love";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Bubbles And Cuddles")
            .SetStats(24, null, 6)
            .SetSprites(
                Absent.GetSprite("BubblesAndCuddles"),
                Absent.GetSprite("BubblesAndCuddlesBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(DreamTeam.NameWhenDeployed(Bubbles.Name, Cuddles.Name))
                ];
                card.charmSlots *= 2;
            });
    }
    
    public bool LeaderExclusive => false;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new();

    public bool InPool => false;
}