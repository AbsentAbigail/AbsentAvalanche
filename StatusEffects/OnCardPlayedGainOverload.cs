using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

internal class OnCardPlayedGainOverload() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "Gain <{a}><keyword=overload>",
    canBoost: true,
    effectToApply: "Overload"
)
{
    public const string Name = "On Turn Gain Overload";
}