using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class OnCardPlayedDrawAndApplyFrenzyAndAimless() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, "<keyword=draw> <{a}> and apply <x{a}><keyword=frenzy> and <keyword=aimless> to it",
    canBoost: true,
    effectToApply: InstantDrawAndApplyFrenzyAndAimless.Name
    )
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}