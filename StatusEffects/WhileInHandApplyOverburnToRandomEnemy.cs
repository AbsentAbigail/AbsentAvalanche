using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhileInHandApplyOverburnToRandomEnemy() : AbstractApplyXStatus<StatusEffectApplyXEveryTurnInHand>(
    Name, "While in hand, every turn apply <{a}><keyword=overload> to a random enemy",
    canBoost: true,
    effectToApply: "Overload",
    applyToFlags: StatusEffectApplyX.ApplyToFlags.RandomEnemy
)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}