using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class TriggerNoTrigger() : AbstractStatus<StatusEffectTriggerWithoutFrenzy>(Name)
{
    public const string Name = "Trigger No Trigger";
}