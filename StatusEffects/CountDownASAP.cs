using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class CountDownASAP() : AbstractStatus<StatusEffectCountDownAsap>(Name, subscribe: status =>
{
    status.effectToApply = AbsentUtils.GetStatus("Reduce Counter");
})
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}