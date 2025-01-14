using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class OnTurnApplyCalmToAllyInFrontOf() : AbstractApplyXStatus<StatusEffectApplyXOnTurn>(
    Name, $"Apply <{{a}}> {Keywords.Calm.Tag} to ally in front",
    canBoost: true,
    effectToApply: Calm.Name,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.AllyInFrontOf
)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}