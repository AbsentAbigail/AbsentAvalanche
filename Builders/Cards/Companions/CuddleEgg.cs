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
public class CuddleEgg : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Cuddle Pip Egg")
            .SetStats(2, null, 1)
            .SetSprites(
                Absent.GetSprite("CuddleEgg"),
                Absent.GetSprite("PipBG"))
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(OnCardPlayedHealAllyInFront.Name),
                    Absent.SStack(ExplorerHealAllies.Name, 15),
                    Absent.SStack(WhenAllyHealedProgressExplorer.Name)
                ];
            });
    }
}