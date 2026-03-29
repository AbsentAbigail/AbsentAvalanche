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
public class LilGuyExplorer2 : ILeaderBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Lil Guy",
                "TargetModeBasic",
                "Blood Profile Snow",
                "SwayAnimationProfile")
            .SetStats(6, 3, 3)
            .SetSprites(
                Absent.GetSprite("LilGuy2"),
                Absent.GetSprite("LilGuy2BG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("On Turn Apply Shell To Self", 3),
                    Absent.SStack(ExplorerDefeatBossFight.Name),
                    Absent.SStack(WhenBossDefeatedProgressExplorer.Name),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "Adventure time!";

    public bool LeaderExclusive => true;

    public bool InPool => false;
    
    public ILeaderBuilder.LeaderModifier LeaderModifiers => new();
}