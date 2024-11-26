using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class WhenAllyHitIncreaseEffects() : AbstractApplyXStatus<StatusEffectApplyXWhenAllyIsHit>(
    Name, "Boost own effects by {a} when an ally is hit",
    effectToApply: "Increase Effects"
    )
{
    public const string Name = "When Ally Hit Increase Effects";
}