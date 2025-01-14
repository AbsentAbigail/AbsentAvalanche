using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class OnCardPlayedTutorRandomTreasure() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "Choose 1 of 3 random items or clunkers to add to your hand",
    effectToApply: InstantTutorThreeRandomTreasures.Name)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}