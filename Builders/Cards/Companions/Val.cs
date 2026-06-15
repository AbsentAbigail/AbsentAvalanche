using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.Scriptables.CardImage;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class Val : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Val",
                "TargetModeBasic",
                "Blood Profile Blue (x2)",
                "FloatAnimationProfile")
            .SetStats(4, 4, 5)
            .SetSprites(
                Absent.GetSprite("Val"),
                Absent.GetSprite("ValBG"))
            .WithFlavour("Has a zipper for a mouth!")
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects = [Absent.SStack(OnHitEat.Name)];
                card.greetMessages =
                [
                    "Has a zipper for a mouth!",
                    "In the mouth of this big blue whale there is room for pajamas or a treasure. It’s because this soft animal is a true friend who can keep a secret, play and give hugs when needed."
                ];

                card.scriptableImagePrefab = Absent.CreateScriptableCardImage<ValCardImage>("val");
            });
    }
}