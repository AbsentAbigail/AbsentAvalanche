using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class InstantDrawAndApplyFrenzyAndAimless() : AbstractStatus<StatusEffectInstantDrawAndApplyX>(
    Name, subscribe: status =>
    {
        status.effects =
        [
            AbsentUtils.GetStatus("Temporary Aimless"),
            AbsentUtils.GetStatus("MultiHit")
        ];
    })
{
    public const string Name = "InstantDrawAndApplyFrenzyAndAimless";
}