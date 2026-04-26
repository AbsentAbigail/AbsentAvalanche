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
            .SetStats(4, 0, 4)
            .SetSprites(
                Absent.GetSprite("Pipsqueak"),
                Absent.GetSprite("PipBG"))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(OnCardPlayedGainCascadingAttack.Name),
                    Absent.SStack(ExplorerDamageEnemies.Name, 150),
                    Absent.SStack(WhenEnemyTakesDamageProgressExplorer.Name),
                ];
            });
    }
}