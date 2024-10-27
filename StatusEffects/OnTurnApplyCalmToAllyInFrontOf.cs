using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

internal class OnTurnApplyCalmToAllyInFrontOf() : AbstractApplyXStatus<StatusEffectApplyXOnTurn>(
    Name, $"Apply <{{a}}> {Keywords.Calm.Tag} to ally in front",
    canBoost: true,
    effectToApply: Calm.Name,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.AllyInFrontOf
)
{
    public const string Name = "On Turn Apply Calm To AllyInFrontOf";
}