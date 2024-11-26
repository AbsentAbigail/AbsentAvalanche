using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Traits;

public class Scavenge() : AbstractTrait(Name, Keywords.Scavenge.Name, OnBossKillGainRandomCharm.Name)
{
    public const string Name = "Scavenge";
}