using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class CountDownEtherealWhenDrawn() : AbstractApplyXStatus<StatusEffectApplyXWhenDrawn>(
    Name,
    effectToApply: InstantCountDownEthereal.Name,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.Self)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}