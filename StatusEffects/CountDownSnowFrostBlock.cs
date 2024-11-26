using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class CountDownSnowFrostBlock() : AbstractStatus<StatusEffectInstantCountDownStatus>(
    Name,
    subscribe: status =>
    {
        status.types = [
            AbsentUtils.GetStatus("Snow").type,
            AbsentUtils.GetStatus("Block").type,
            AbsentUtils.GetStatus("Frost").type
        ];
    })
{
    public const string Name = "CountDown Snow Frost Block";
}