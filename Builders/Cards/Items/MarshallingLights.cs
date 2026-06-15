using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class MarshallingLights : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Marshalling Lights")
            .SetDamage(null)
            .SetSprites(
                Absent.GetSprite("MarshallingLights"),
                Absent.GetSprite("MarshallingLightsBG"))
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(OnCardPlayedIncreaseAlliesAttackEqualToFlight.Name),
                    Absent.SStack(OnCardPlayedCountDownAlliesFlight.Name),
                ];

                card.traits =
                [
                    Absent.TStack("Consume")
                ];
            });
    }
}