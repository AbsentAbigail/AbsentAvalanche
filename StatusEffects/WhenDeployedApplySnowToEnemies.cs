using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhenDeployedApplySnowToEnemies() : AbstractApplyXStatus<StatusEffectApplyXWhenDeployed>(
    Name, "When deployed, apply <{a}><keyword=snow> to all enemies",
    canBoost: true, applyToFlags: StatusEffectApplyX.ApplyToFlags.Enemies)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}