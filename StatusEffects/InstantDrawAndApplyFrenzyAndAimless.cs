using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class InstantDrawAndApplyFrenzyAndAimless() : AbstractStatus<StatusEffectInstantDrawAndApplyX>(
    Name, subscribe: status =>
    {
        status.effects =
        [
            AbsentUtils.GetStatus("Temporary Aimless"),
            AbsentUtils.GetStatus("MultiHit")
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}