using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

internal class OnHitGainEqualBling() : AbstractApplyXStatus<StatusEffectApplyXOnHit>(
    Name, "Gain <keyword=blings> equal to damage dealt",
    effectToApply: "Gain Gold",
    subscribe: status => { status.applyEqualAmount = true; })
{
    public const string Name = "On Hit Gain Equal Bling";
}