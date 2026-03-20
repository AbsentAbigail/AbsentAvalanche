#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class RescueUfo : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Rescue UFO")
            .SetDamage(null)
            .SetSprites(Absent.GetSprite("RescueUFO"), Absent.GetSprite("RescueUFOBG"))
            .WithPools(CardPools.GeneralItems)
            .WithValue(40)
            .CanPlayOnHand(false)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects = [Absent.SStack(Abduct.Name)];
                card.traits =
                [
                    Absent.TStack("Consume"),
                    Absent.TStack("Zoomlin")
                ];
            });
    }
}