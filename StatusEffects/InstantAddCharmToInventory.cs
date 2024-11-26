using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class InstantAddCharmToInventory() : AbstractStatus<StatusEffectInstantAddRandomCharm>(Name)
{
    public const string Name = "Instant Add Charm To Inventory";
}