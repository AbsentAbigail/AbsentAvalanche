using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class OnCardPlayedTutorRandomTreasure() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "Add 1 of 3 random items or clunker to your hand",
    effectToApply: InstantTutorThreeRandomTreasures.Name)
{
    public const string Name = "OnCardPlayedTutorRandomTreasure";
}