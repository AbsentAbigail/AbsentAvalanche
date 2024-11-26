using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class InstantPermanentlyIncreaseHealth() : AbstractStatus<StatusEffectInstantChangeStatsPermanent>(Name)
{
    public const string Name = "Instant Permanently Increase Health";
}