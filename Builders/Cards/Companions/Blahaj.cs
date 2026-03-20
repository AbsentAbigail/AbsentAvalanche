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
public class Blahaj : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Blåhaj",
                "TargetModeBasic",
                "Blood Profile Pink Wisp")
            .SetStats(9, null, 3)
            .SetSprites(
                Absent.GetSprite("Blahaj"),
                Absent.GetSprite("BlahajBG"))
            .WithFlavour("Accepts and loves you <3")
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects = [Absent.SStack(OnTurnApplyCalmToAllyInFrontOf.Name, 2)];
                card.greetMessages =
                [
                    "Big and safe to have by your side if you want to discover the world below the surface of the ocean. The blue shark can swim very far, dive really deep and hear noises from almost 250 metres away.",
                    "Accepts and loves you <3",
                    "*shark noises*"
                ];
            });
    }
}