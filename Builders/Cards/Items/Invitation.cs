using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class Invitation : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Invitation")
            .SetDamage(null)
            .SetSprites(
                Absent.GetSprite("Invitation"),
                Absent.GetSprite("InvitationBG"))
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Absent.SStack(InstantDoNothing.Name), // Attack effect so that the card has a target for the other effect
                ];
                card.startWithEffects =
                [
                    Absent.SStack(StatusEffects.Invitation.Name),
                ];
                card.charmSlots = 0;
            });
    }
}