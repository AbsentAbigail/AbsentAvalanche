using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class InstantTutorDiscard() : AbstractStatus<StatusEffectInstantTutor>(Name, subscribe: status =>
{
    status.source = StatusEffectInstantTutor.CardSource.Discard;
})
{
    public const string Name = "InstantTutorDiscard";
}