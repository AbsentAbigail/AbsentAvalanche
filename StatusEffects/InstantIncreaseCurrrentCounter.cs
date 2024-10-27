using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

internal class InstantIncreaseCurrentCounter() : AbstractStatus<StatusInstantIncreaseCounter>(
    Name, "Count <keyword=counter> up by <{a}>",
    canBoost: true,
    subscribe: status => status.type = "counter down"
)
{
    public const string Name = "Instant Increase Current Counter";
}