using AbsentAvalanche.Keywords;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class OnCardPlayedApplyRandomBuffToRandomAlly() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, $"Apply <{{a}}> {RainbowFluff.Tag} to a random ally",
    canBoost: true, effectToApply: InstantRandomBuff.Name,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.RandomAlly
    )
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}