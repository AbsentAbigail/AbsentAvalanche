using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.StatusEffects;

public class InstantTutorThreeRandomTreasures() : AbstractStatus<StatusEffectInstantTutor>(Name, subscribe: status =>
{
    status.source = StatusEffectInstantTutor.CardSource.Custom;
    status.summonCopy = AbsentUtils.GetStatusOf<StatusEffectInstantSummon>(InstantSummonDummyToHand.Name);
    status.amount = 3;
    status.Predicate = cardData =>
        cardData.cardType.name == "Item" || (cardData.cardType.name == "Clunker" && !cardData.IsPet());
})
{
    public const string Name = "InstantTutorThreeRandomTreasures";
}