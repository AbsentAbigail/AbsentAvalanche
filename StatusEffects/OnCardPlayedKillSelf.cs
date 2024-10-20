using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

internal class OnCardPlayedKillSelf() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "Destroy self",
    effectToApply: "Kill"
)
{
    public const string Name = "On Card Played Kill Self";
}