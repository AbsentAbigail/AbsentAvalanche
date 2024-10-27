using AbsentAvalanche.StatusEffects.TargetModes;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

internal class HitsAllAlliesAndEnemies() : AbstractStatus<StatusEffectChangeTargetMode>(
    Name, "Hits all allies and enemies",
    subscribe: status =>
    {
        status.targetMode = ScriptableHelper.CreateScriptable<TargetModeAlliesAndEnemies>("Hit All Allies And Enemies");
    })
{
    public const string Name = "Hit All Allies And Enemies";
}