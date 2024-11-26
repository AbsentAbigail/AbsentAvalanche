using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class OnCardPlayedBoostSelf() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "Increase by 1", effectToApply: "Increase Effects")
{
    public const string Name = "OnCardPlayedBoostSelf"; 
}