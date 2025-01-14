using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.Cards.Items;
using AbsentUtilities;
using HarmonyLib;

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
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}