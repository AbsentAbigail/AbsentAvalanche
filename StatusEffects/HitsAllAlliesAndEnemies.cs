using AbsentAvalanche.StatusEffects.TargetModes;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class HitsAllAlliesAndEnemies() : AbstractStatus<StatusEffectChangeTargetMode>(
    Name, "Hits all allies and enemies",
    subscribe: status =>
    {
        status.targetMode = ScriptableHelper.CreateScriptable<TargetModeAlliesAndEnemies>("Hit All Allies And Enemies");
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}