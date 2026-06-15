using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class Chakram : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Chakram")
            .SetDamage(null)
            .SetSprites(Absent.GetSprite("Chakram"), Absent.GetSprite("ChakramBG"))
            .WithPools(CardPools.GeneralItems)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Absent.SStack(Flight.Name),
                    Absent.SStack(InstantDealDamageEqualToFlightToEnemiesInRow.Name),
                ];
                card.startWithEffects =
                [
                    Absent.SStack(OnKillGainFrenzy.Name)
                ];
            });
    }
}