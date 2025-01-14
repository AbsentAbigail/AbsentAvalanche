using AbsentAvalanche.Cards.Items;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class OnTurnSummonUfoInHand() : AbstractApplyXStatus<StatusEffectApplyXOnTurn>(
    Name, "Add <{a}> {0} to your hand",
    canBoost: true,
    effectToApply: InstantSummonUfoInHand.Name,
    subscribe: status => status.textInsert = AbstractCard.CardTag(RescueUFO.Name))
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}