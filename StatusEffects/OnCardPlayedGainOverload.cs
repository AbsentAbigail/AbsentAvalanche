using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class OnCardPlayedGainOverload() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "Gain <{a}><keyword=overload>",
    canBoost: true,
    effectToApply: "Overload"
)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}