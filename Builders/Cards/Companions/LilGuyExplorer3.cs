#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class LilGuyExplorer3 : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Lil Guy",
                "TargetModeBasic",
                "Blood Profile Snow",
                "SwayAnimationProfile")
            .SetStats(8, 4, 3)
            .SetSprites(
                Absent.GetSprite("LilGuy3"),
                Absent.GetSprite("LilGuy3BG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("On Turn Apply Shell To Self", 3),
                    Absent.SStack("MultiHit")
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Adventure time!";

    public bool LeaderExclusive => true;

    public bool InPool => false;
    
    public ILeaderBuilder.LeaderModifier LeaderModifiers => new();
}