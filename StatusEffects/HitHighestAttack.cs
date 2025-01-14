using AbsentAvalanche.StatusEffects.TargetModes;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class HitHighestAttack() : AbstractStatus<StatusEffectChangeTargetMode>(
    Name,
    subscribe: status =>
        status.targetMode = ScriptableHelper.CreateScriptable<TargetModeHighestAttack>("Highest Attack")
)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}