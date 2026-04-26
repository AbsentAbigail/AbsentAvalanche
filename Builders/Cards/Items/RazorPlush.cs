#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class RazorPlush : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Razor Plushie")
            .SetDamage(null)
            .NeedsTarget(false)
            .SetSprites(Absent.GetSprite("RazorPlushie"), Absent.GetSprite("HogRazorBG"))
            .WithPools(CardPools.GeneralItems)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects = [Absent.SStack(OnCardPlayedAddHogCosplayToHand.Name, 3)];
                card.traits = [Absent.TStack("Consume")];
            });
    }
}