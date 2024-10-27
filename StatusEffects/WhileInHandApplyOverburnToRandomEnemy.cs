using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class WhileInHandApplyOverburnToRandomEnemy() : AbstractApplyXStatus<StatusEffectApplyXEveryTurnInHand>(
    Name, "While in hand, every turn apply <{a}><keyword=overload> to a random enemy",
    canBoost: true,
    effectToApply: "Overload",
    applyToFlags: StatusEffectApplyX.ApplyToFlags.RandomEnemy
)
{
    public const string Name = "While In Hand Apply Overload To RandomEnemy";
}