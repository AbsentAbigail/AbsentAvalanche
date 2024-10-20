using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Traits;

public class Trample() : AbstractTrait(Name, Keywords.Trample.Name, OnKillTriggerNoTrigger.Name)
{
    public const string Name = "Trample";
}