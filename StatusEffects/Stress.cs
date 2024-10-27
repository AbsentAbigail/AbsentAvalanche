using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

internal class Stress() : AbstractStatus<StatusEffectStress>(
    Name, "Deal <{a}> additional damage for each damaged ally",
    canBoost: true,
    subscribe: status => status.on = StatusEffectBonusDamageEqualToX.On.Board
)
{
    public const string Name = "Stress";
}