using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class TemporarySafeZoomlin() : AbstractStatus<StatusEffectSafeTemporaryTrait>(Name, subscribe: status => 
    status.trait = AbsentUtils.GetTrait("Zoomlin"))
{
    public const string Name = "TemporarySafeZoomlin";
}