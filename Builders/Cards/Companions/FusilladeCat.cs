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
public class FusilladeCat : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public const string Flavour = "A cat with an explosive personality";

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Fusillade Cat",
                bloodProfile: "Blood Profile Black",
                idleAnim: "FloatAnimationProfile")
            .SetStats(9, 0, 3)
            .SetSprites(
                Absent.GetSprite("FusilladeCat"),
                Absent.GetSprite("FusilladeCatBG"))
            .WithFlavour(Flavour)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack(Cat.Name),
                    Absent.SStack("Weakness"),
                    Absent.SStack(WhenEnemyIsHitByItemApplyWeaknessToThem.Name),
                    Absent.SStack(GainCatWhenItemIsPlayed.Name),
                    Absent.SStack(OnCardPlayedAddMissileToHand.Name, 2)
                ];
            });
    }
}