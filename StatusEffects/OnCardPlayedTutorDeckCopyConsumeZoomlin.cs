using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class OnCardPlayedTutorDeckCopyConsumeZoomlin() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "Add a copy of a card in your draw pile to your hand and apply <keyword=zoomlin> and <keyword=consume> to it",
    effectToApply: InstantTutorDeckCopyZoomlinConsume.Name)
{
    public const string Name = "OnCardPlayedTutorDeckConsumeZoomlin";
}