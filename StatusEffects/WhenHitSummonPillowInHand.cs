using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.Cards.Items;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class WhenHitSummonPillow() : AbstractApplyXStatus<StatusEffectApplyXWhenHit>(
    Name, "When hit, add {0} to your hand",
    canBoost: true,
    effectToApply: InstantSummonPillowInHand.Name,
    subscribe: status =>
    {
        status.textInsert = "<{a}> " + AbstractCard.CardTag(Pillow.Name);
        status.targetMustBeAlive = false;
    })
{
    public const string Name = "WhenHitSummonPillow";
}