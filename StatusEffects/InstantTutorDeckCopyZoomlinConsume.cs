using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class InstantTutorDeckCopyZoomlinConsume() : AbstractStatus<StatusEffectInstantTutor>(Name, subscribe: status =>
{
    status.summonCopy = AbsentUtils.GetStatusOf<StatusEffectInstantSummon>(InstantSummonDummyToHand.Name);
    status.addEffectStacks = [
        AbsentUtils.SStack(TemporarySafeZoomlin.Name),
        AbsentUtils.SStack(TemporarySafeConsume.Name),
    ];
})
{
    public const string Name = "InstantTutorDeckCopyZoomlinConsume";
}