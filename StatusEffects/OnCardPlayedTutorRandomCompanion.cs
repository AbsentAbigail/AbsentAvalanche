using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class OnCardPlayedTutorRandomCompanion() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "Choose 1 of 3 random companions to add to your hand",
    effectToApply: InstantTutorThreeRandomCompanions.Name)
{
    public const string Name = "OnCardPlayedTutorRandomCompanion";
}