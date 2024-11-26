using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Companion;

public class Alice() : AbstractCompanion(Name, "Alice", 3, 2, 5,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(WhenEnemyIsKilledGainBlock.Name),
            AbsentUtils.SStack(WhileActiveGainFrenzyEqualToBlock.Name)
        ];
    })
{
    public const string Name = "Alice";
    public override string FlavourText => "*Cozy paca noises*";
    protected override string BloodProfile => "Blood Profile Snow";
}