using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class TemporarySafeConsume() : AbstractStatus<StatusEffectSafeTemporaryTrait>(Name, subscribe: status => 
    status.trait = AbsentUtils.GetTrait("Consume"))
{
    public const string Name = "TemporarySafeConsume";
}