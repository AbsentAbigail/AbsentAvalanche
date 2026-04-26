#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class BiglooCosplay : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Bigloo Cosplay")
            .SetDamage(null)
            .SetSprites(Absent.GetSprite("BiglooCosplay"), Absent.GetSprite("BiglooCosplayBG"))
            .WithValue(50)
            .CanPlayOnHand(false)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Absent.SStack("Snow")
                ];
                card.startWithEffects =
                [
                    Absent.SStack(Equip.Name)
                ];
                card.traits =
                [
                    Absent.TStack("Barrage")
                ];
            });
    }
}