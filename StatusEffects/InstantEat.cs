using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class InstantEat() : AbstractStatus<StatusInstantEatCard>(
    Name,
    subscribe: status =>
    {
        status.effectToApply = AbsentUtils.GetStatus("Kill");

        status.illegalEffects =
        [
            AbsentUtils.GetStatus("On Turn Escape To Self"),
            AbsentUtils.GetStatus("Scrap")
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}