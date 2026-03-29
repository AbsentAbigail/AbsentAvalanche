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
public class LeafPipsqueak : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Pipsqueak")
            .SetStats(6, 4, 3)
            .SetSprites(
                Absent.GetSprite("Pipsqueak"),
                Absent.GetSprite("PipBG"))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("While Active Increase Attack To Allies"),
                    Absent.SStack(ExplorerOverkill.Name, 100),
                    Absent.SStack(WhenEnemyOverkilledProgressExplorer.Name),
                ];
            });
    }
}