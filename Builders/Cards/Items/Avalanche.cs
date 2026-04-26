#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class Avalanche : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Avalanche")
            .WithFlavour("Absent Avalanche")
            .SetDamage(0)
            .SetSprites(Absent.GetSprite("Avalanche"), Absent.GetSprite("AvalancheBG"))
            .WithPools(CardPools.GeneralItems)
            .CanPlayOnHand(false)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Absent.SStack(InstantCascadingSnow.Name)
                ];
            });
    }
}