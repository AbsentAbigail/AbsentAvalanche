using AbsentAvalanche.StatusEffects.TargetModes;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class HitHighestAttack() : AbstractStatus<StatusEffectChangeTargetMode>(
    Name,
    subscribe: status =>
        status.targetMode = ScriptableHelper.CreateScriptable<TargetModeHighestAttack>("Highest Attack")
)
{
    public const string Name = "Hit Highest Attack";
}