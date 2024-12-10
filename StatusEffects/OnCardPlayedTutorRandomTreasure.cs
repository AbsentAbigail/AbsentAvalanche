using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class OnCardPlayedTutorRandomTreasure() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "Choose 1 of 3 random items or clunkers to add to your hand",
    effectToApply: InstantTutorThreeRandomTreasures.Name)
{
    public const string Name = "OnCardPlayedTutorRandomTreasure";
}