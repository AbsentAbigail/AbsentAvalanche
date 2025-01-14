using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class InstantDoubleCat() : AbstractStatus<StatusEffectInstantDoubleX>(
    Name,
    subscribe: status =>
    {
        status.statusToDouble = AbsentUtils.GetStatus(Cat.Name);
        status.countsAsHit = false;
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}