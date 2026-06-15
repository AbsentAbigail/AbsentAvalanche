using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class Coolant : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Coolant")
            .SetDamage(0)
            .SetSprites(
                Absent.GetSprite("Coolant"),
                Absent.GetSprite("CoolantBG"))
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Absent.SStack("Snow")
                ];
                card.startWithEffects =
                [
                    Absent.SStack(WhenAllyRecalledGainApplySnow.Name)
                ];
            });
    }
}