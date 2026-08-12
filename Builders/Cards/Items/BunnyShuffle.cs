using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Cards.Items;

[UsedImplicitly]
public class BunnyShuffle : ICardBuilder
{
    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateItem(Name, "Bunny Shuffle")
            .SetDamage(null)
            .SetSprites(
                Absent.GetSprite("BunnyShuffle"),
                Absent.GetSprite("BunnyShuffleBG"))
            .WithPools(CardPools.GeneralItems)
            .WithValue(50)
            .CanPlayOnHand()
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.attackEffects =
                [
                    Absent.SStack(InstantSwapAttackHealth.Name),
                ];
                card.traits =
                [
                    Absent.TStack("Consume"),
                ];
            });
    }
    
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}