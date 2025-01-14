using AbsentAvalanche.Keywords;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhenDeployedApplyRandomBuffToAllAllies() : AbstractApplyXStatus<StatusEffectApplyXWhenDeployed>(
    Name, $"When deployed, apply <{{a}}> {RainbowFluff.Tag} to all allies",
    canBoost: true, effectToApply: InstantRandomBuff.Name,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.Allies
    )
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}