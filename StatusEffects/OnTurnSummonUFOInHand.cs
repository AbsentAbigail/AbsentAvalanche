using AbsentAvalanche.Cards.Items;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

internal class OnTurnSummonUFOInHand() : AbstractApplyXStatus<StatusEffectApplyXOnTurn>(
    Name, "Add <{a}> {0} to your hand",
    canBoost: true,
    effectToApply: InstantSummonUFOInHand.Name,
    subscribe: status => status.textInsert = AbstractCard.CardTag(RescueUFO.Name))
{
    public const string Name = "On Turn Summon UFO In Hand";
}