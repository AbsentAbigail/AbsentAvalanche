using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class WhenAllyHitGainFrenzy() : AbstractApplyXStatus<StatusEffectApplyXWhenAllyIsHit>(
    Name, "When an ally is hit, gain {0}",
    canBoost: true,
    effectToApply: "MultiHit",
    subscribe: status => status.textInsert = "<x{a}><keyword=frenzy>"
    )
{
    public const string Name = "When Ally Hit Gain Frenzy";
}