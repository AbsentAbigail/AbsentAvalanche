using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhenDestroyedApplyWeaknessToAllies() : AbstractApplyXStatus<StatusEffectApplyXWhenDestroyed>(
    Name, "When destroyed, apply <{a}><keyword=weakness> to all allies",
    canBoost: true,
    effectToApply: "Weakness",
    applyToFlags: StatusEffectApplyX.ApplyToFlags.Allies,
    subscribe: status =>
    {
        status.targetMustBeAlive = false;
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}