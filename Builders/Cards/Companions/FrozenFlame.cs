#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class FrozenFlame : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "How did this even happen?";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Frozen Flame",
                bloodProfile: "Blood Profile Black",
                idleAnim: "FloatAnimationProfile")
            .SetStats(1, null, 1)
            .SetSprites(
                Absent.GetSprite("FrozenFlame"),
                Absent.GetSprite("FrozenFlameBG"))
            .WithFlavour(Flavour)
            .WithPools(CardPools.ShademancerUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(OnCardPlayedGainOverload.Name),
                    Absent.SStack("Block", 4),
                    Absent.SStack(WhenDestroyedSummonUnboundFlame.Name)
                ];
                card.greetMessages =
                [
                    "This damned frost... Hey you, help me out!",
                    "Is it cold here, or is that just me?",
                    "Who touched the thermostat?!"
                ];
            });
    }
}