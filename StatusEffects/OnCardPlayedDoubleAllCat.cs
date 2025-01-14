using AbsentUtilities;
using HarmonyLib;
using static StatusEffectApplyX.ApplyToFlags;

namespace AbsentAvalanche.StatusEffects;

public class OnCardPlayedDoubleAllCat() : AbstractApplyXStatus<StatusEffectApplyXOnCardPlayed>(
    Name, $"Double <ALL> {Keywords.Cat.Tag}",
    effectToApply: InstantDoubleCat.Name,
    applyToFlags: Allies | Self | Enemies | Hand
    )
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}