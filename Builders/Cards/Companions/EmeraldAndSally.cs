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
public class EmeraldAndSally : ILeaderBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "So smol, so big";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Smol bear, big bear")
            .SetStats(13, 0, 3)
            .SetSprites(
                Absent.GetSprite("EmeraldSally"),
                Absent.GetSprite("EmeraldSallyBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(DreamTeam.NameWhenDeployed(Emerald.Name, Sally.Name))
                ];
                card.charmSlots *= 2;
            });
    }
    
    public bool LeaderExclusive => false;

    public bool InPool => false;

    public ILeaderBuilder.LeaderModifier LeaderModifiers => new();
}