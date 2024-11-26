using System.Linq;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class InstantTutorTenRandomCardsZoomlin() : AbstractStatus<StatusEffectInstantTutor>(Name, subscribe: status =>
{
    status.source = StatusEffectInstantTutor.CardSource.Custom;
    status.summonCopy = AbsentUtils.GetStatusOf<StatusEffectInstantSummon>(InstantSummonDummyToHand.Name);
    status.amount = 10;
    
    status.customCardList = AddressableLoader.GetGroup<CardData>("CardData")
        .Where(c =>
            c.cardType.name == "Item" &&
            c.playType != Card.PlayType.None &&
            c.mainSprite?.name != "Nothing" &&
            (c.traits == null || !c.traits.Exists(b => b.data.name is "Recycle")))
        .Select(c => c.name).ToArray();
    
    status.addEffectStacks = [AbsentUtils.SStack(TemporarySafeZoomlin.Name)];
})
{
    public const string Name = "InstantTutorTenRandomCardsZoomlin";
}