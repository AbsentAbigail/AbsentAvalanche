using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class WhenDestroyedSummonSarcophagus() : AbstractApplyXStatus<StatusEffectApplyXWhenDestroyed>(
    Name, "Add <{a}> {0} to hand when destroyed",
    canBoost: true,
    effectToApply: InstantSummonSarcophagus.Name,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.Self,
    subscribe: status =>
    {
        status.targetMustBeAlive = false;
        status.textInsert = "<of the sealed card>";
    })
{
    public const string Name = "When Destroyed Summon Sarcophagus";
}