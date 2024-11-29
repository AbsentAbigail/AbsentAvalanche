using System.Linq;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class InstantTutorTenRandomCardsZoomlin() : AbstractStatus<StatusEffectInstantTutor>(Name, subscribe: status =>
{
    status.source = StatusEffectInstantTutor.CardSource.Custom;
    status.summonCopy = AbsentUtils.GetStatusOf<StatusEffectInstantSummon>(InstantSummonDummyToHand.Name);
    status.amount = 10;

    status.Predicate = cardData => 
        cardData.cardType.name == "Item" &&
        cardData.playType != Card.PlayType.None &&
        (cardData.traits is null || !cardData.traits.Exists(b => b.data.name is "Recycle"));
    
    status.addEffectStacks = [AbsentUtils.SStack(TemporarySafeZoomlin.Name)];
})
{
    public const string Name = "InstantTutorTenRandomCardsZoomlin";
}