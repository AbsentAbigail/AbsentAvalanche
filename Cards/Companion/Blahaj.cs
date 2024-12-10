using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

internal class Blahaj() : AbstractCompanion(
    Name, "Blåhaj",
    9, null, 3,
    subscribe: card =>
    {
        card.startWithEffects = [AbsentUtils.SStack(OnTurnApplyCalmToAllyInFrontOf.Name, 2)];
        card.greetMessages =
        [
            "Big and safe to have by your side if you want to discover the world below the surface of the ocean. The blue shark can swim very far, dive really deep and hear noises from almost 250 metres away.",
            "Accepts and loves you <3",
            "*shark noises*"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Accepts and loves you <3";
    protected override string BloodProfile => "Blood Profile Pink Wisp";
}