using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

internal class InstantEat() : AbstractStatus<StatusInstantEatCard>(
    Name,
    subscribe: status =>
    {
        status.effectToApply = AbsentUtils.GetStatus("Kill");

        status.illegalEffects =
        [
            AbsentUtils.GetStatus("On Turn Escape To Self"),
            AbsentUtils.GetStatus("Scrap")
        ];
    })
{
    public const string Name = "Instant Eat";
}