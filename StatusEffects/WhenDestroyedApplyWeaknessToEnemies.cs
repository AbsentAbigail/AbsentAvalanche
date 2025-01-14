using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhenDestroyedApplyWeaknessToEnemies() : AbstractApplyXStatus<StatusEffectApplyXWhenDestroyed>(
    Name, "When destroyed, apply <{a}><keyword=weakness> to all enemies",
    canBoost: true,
    effectToApply: "Weakness",
    applyToFlags: StatusEffectApplyX.ApplyToFlags.Enemies,
    subscribe: status =>
    {
        status.targetMustBeAlive = false;
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}