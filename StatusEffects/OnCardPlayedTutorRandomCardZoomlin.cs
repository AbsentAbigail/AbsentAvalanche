using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class OnCardPlayedTutorRandomCardZoomlin() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "Add 1 of 10 random cards to your hand and apply <keyword=zoomlin> to it",
    effectToApply: InstantTutorTenRandomCardsZoomlin.Name)
{
    public const string Name = "OnCardPlayedTutorRandomCardZoomlin";
}