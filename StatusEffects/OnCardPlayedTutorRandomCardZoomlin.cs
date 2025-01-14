using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class OnCardPlayedTutorRandomCardZoomlin() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "Choose 1 of 10 random cards to add to your hand and apply <keyword=zoomlin> to it",
    effectToApply: InstantTutorTenRandomCardsZoomlin.Name)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}