using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class EveryTurnCountDownSnowFrostBlock() : AbstractApplyXStatus<StatusEffectApplyXEveryTurn>(
    Name,
    effectToApply: InstantCountDownSnowFrostBlock.Name,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.AlliesInRow
    )
{
    public const string Name = "Every Turn Count Down Snow Frost Block";
}