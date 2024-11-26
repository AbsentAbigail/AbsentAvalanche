using AbsentAvalanche.Cards.Companion;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class WhenHitSummonBlahaj() : AbstractApplyXStatus<StatusEffectApplyXWhenHit>(
    Name, "When hit, summon {0}",
    effectToApply: InstantSummonBlahaj.Name,
    subscribe: status =>
    {
        status.textInsert = AbstractCard.CardTag(Blahaj.Name);
        status.targetMustBeAlive = false;
    })
{
    public const string Name = "When Hit Summon Blahaj";
}