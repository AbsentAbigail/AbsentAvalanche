#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class RingerCosplay : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Ringer Cosplay")
            .SetHealth(1)
            .SetDamage(1)
            .SetSprites(Absent.GetSprite("RingerCosplay"), Absent.GetSprite("RingerCosplayBG"))
            .WithValue(50)
            .CanPlayOnHand(false)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(Equip.Name),
                    Absent.SStack("When Hit Apply Frost To RandomEnemy", 2)
                ];
            });
    }
}