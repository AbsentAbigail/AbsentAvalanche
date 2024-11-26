using AbsentUtilities;
using static StatusEffectApplyX.ApplyToFlags;

namespace AbsentAvalanche.StatusEffects;

public class OnCardPlayedDoubleAllCat() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, $"Double <ALL> {Keywords.Cat.Tag}",
    effectToApply: InstantDoubleCat.Name,
    applyToFlags: Allies | Self | Enemies | Hand
    )
{
    public const string Name = "On Card Played Double All Cat";
}