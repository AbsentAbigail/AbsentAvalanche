using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class WhenKilledInsteadGainScrap() : AbstractApplyXStatus<StatusEffectWhenDestroyedInsteadX>(
    Name, "When killed, instead gain <{a}><keyword=scrap>",
    canBoost: true,
    effectToApply: "Scrap",
    applyToFlags: StatusEffectApplyX.ApplyToFlags.Self,
    subscribe: status => { status.resetHealth = true; })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}