using AbsentAvalanche.Cards.Companion;
using AbsentUtilities;
using HarmonyLib;

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
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}