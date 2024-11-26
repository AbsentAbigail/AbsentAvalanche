using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

internal class InstantDoubleCat() : AbstractStatus<StatusEffectInstantDoubleX>(
    Name,
    subscribe: status =>
    {
        status.statusToDouble = AbsentUtils.GetStatus(Cat.Name);
        status.countsAsHit = false;
    })
{
    public const string Name = "Instant Double Cat";
}