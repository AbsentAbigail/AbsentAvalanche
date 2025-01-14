using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class EveryTurnCountDownSnowFrostBlock() : AbstractApplyXStatus<StatusEffectApplyXEveryTurn>(
    Name,
    effectToApply: InstantCountDownSnowFrostBlock.Name,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.AlliesInRow
    )
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}