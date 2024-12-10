using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class OnCardPlayedGainSnow()
    : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(Name, "Gain <{a}><keyword=snow>", canBoost: true)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}