using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Traits;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class Togepi : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Togepi")
            .SetDamage(0)
            .SetSprites(
                Absent.GetSprite("Togepi"),
                Absent.GetSprite("TogepiBG"))
            .WithValue(50)
            .WithPools(CardPools.GeneralItems)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.traits =
                [
                    Absent.TStack(Metronome.Name)
                ];
            });
    }
}