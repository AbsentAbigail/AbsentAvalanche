using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class TriggerWhenAllyBehindTriggers() : AbstractStatus<StatusEffectApplyXWhenAllyBehindTriggers>(
    Name, "Trigger when ally behind attacks",
    subscribe: status =>
    {
        status.isReaction = true;
        status.descColorHex = "F99C61";
    })
{
    public const string Name = "Trigger When Ally Behind Triggers";
}