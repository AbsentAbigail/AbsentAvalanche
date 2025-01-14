using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class OnCardPlayedTutorDeckCopyConsumeZoomlin() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "Choose a card in your draw pile, add a copy of it to your hand with <keyword=zoomlin> and <keyword=consume>",
    effectToApply: InstantTutorDeckCopyZoomlinConsume.Name)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}