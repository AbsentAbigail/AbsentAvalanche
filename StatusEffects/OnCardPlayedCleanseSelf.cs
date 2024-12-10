using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class OnCardPlayedCleanseSelf() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "<keyword=cleanse> self",
    effectToApply: "Cleanse")
{
    public const string Name = "OnCardPlayedCleanseSelf";
}