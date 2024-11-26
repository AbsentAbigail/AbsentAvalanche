using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

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
    })
{
    public const string Name = "Jerry";
    public override string FlavourText => "Accepts and loves you <5";
    protected override string BloodProfile => "Blood Profile Pink Wisp";
}