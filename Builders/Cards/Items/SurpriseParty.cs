#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class SurpriseParty : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Surprise Party", idleAnim: "Heartbeat2AnimationProfile")
            .SetDamage(1)
            .NeedsTarget(false)
            .SetSprites(Absent.GetSprite("SurpriseParty"), Absent.GetSprite("SurprisePartyBG"))
            .WithPools(CardPools.GeneralItems)
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("Hit All Enemies"),
                    Absent.SStack(TriggerWhenDrawn.Name)
                ];
                card.traits =
                [
                    Absent.TStack("Consume")
                ];
            });
    }
}