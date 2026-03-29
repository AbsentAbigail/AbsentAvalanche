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
public class CuddlePipsqueak : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Cuddle Pipsqueak")
            .SetStats(3, null, 4)
            .SetSprites(
                Absent.GetSprite("Cuddlesqueak"),
                Absent.GetSprite("PipBG"))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("On Turn Heal Allies", 3),
                    Absent.SStack(ExplorerHealAllies.Name, 150),
                    Absent.SStack(WhenAllyHealedProgressExplorer.Name)
                ];
            });
    }
}