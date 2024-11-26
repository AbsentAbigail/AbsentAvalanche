using AbsentUtilities;
using static StatusEffectApplyX.ApplyToFlags;

namespace AbsentAvalanche.StatusEffects;

public class WhenAnythingSnowedGainBlock() : AbstractApplyXStatus<StatusEffectApplyXWhenYAppliedTo>(
    Name, "When anything is <keyword=snow>'d, gain {0}",
    effectToApply: "Block",
    canBoost: true,
    subscribe: status =>
    {
        status.textInsert = "<{a}><keyword=block>";
        status.whenAppliedToFlags = Self | Allies | Enemies;
    })
{
    public const string Name = "When Anything Snowed Gain Block";
}