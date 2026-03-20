#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Builders.Traits;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class DrawingPractice : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Drawing Practice")
            .SetDamage(null)
            .NeedsTarget(false)
            .SetSprites(Absent.GetSprite("DrawingPractice"), Absent.GetSprite("DrawingPracticeBG"))
            .WithPools(CardPools.ClunkmasterItems)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects = [Absent.SStack(WhenConsumedInkAllEnemies.Name, 8)];
                card.traits =
                [
                    Absent.TStack(Combo.Name, 2),
                    Absent.TStack("Consume")
                ];
            });
    }
}