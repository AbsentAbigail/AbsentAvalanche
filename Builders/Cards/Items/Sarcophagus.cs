#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class Sarcophagus : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Sarcophagus", idleAnim: "PulseAnimationProfile")
            .SetDamage(null)
            .NeedsTarget(false)
            .SetSprites(Absent.GetSprite("Sarcophagus"), Absent.GetSprite("SarcophagusBG"))
            .WithValue(50)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(Ethereal.Name, 3),
                    Absent.SStack(WhenDestroyedSummonSarcophagus.Name, 2)
                ];
            });
    }
}