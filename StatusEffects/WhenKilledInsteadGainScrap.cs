using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

internal class WhenKilledInsteadGainScrap() : AbstractApplyXStatus<StatusEffectWhenDestroyedInsteadX>(
    Name, "When killed, instead gain <{a}><keyword=scrap>",
    canBoost: true,
    effectToApply: "Scrap",
    applyToFlags: StatusEffectApplyX.ApplyToFlags.Self,
    subscribe: status => { status.resetHealth = true; })
{
    public const string Name = "When Destroyed Instead Gain Scrap";
}