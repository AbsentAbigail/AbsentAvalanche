#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Clunkers;

[UsedImplicitly]
public class PillowFortress : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "A stronghold to ward of evil, or to sleep in";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Pillow Fortress", idleAnim: "PulseAnimationProfile")
            .WithCardType("Clunker")
            .SetHealth(null)
            .SetDamage(null)
            .SetSprites(Absent.GetSprite("PillowFortress"), Absent.GetSprite("PillowFortressBG"))
            .WithFlavour(Flavour)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("Scrap", 2),
                    Absent.SStack(WhenHitSummonPillow.Name)
                ];
            });
    }
}