using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

internal class Jerry() : AbstractCompanion(
    Name, "Jerry",
    2, 3, 5,
    subscribe: card =>
    {
        card.startWithEffects = [
            AbsentUtils.SStack(FakeCalm.Name, 6),
            AbsentUtils.SStack(WhenHitSummonBlahaj.Name),
        ];
        card.greetMessages =
        [
            "That's a lot of sharks"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Accepts and loves you <5";
    protected override string BloodProfile => "Blood Profile Pink Wisp";
}