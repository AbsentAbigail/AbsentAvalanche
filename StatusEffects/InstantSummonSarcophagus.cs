using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class InstantSummonSarcophagus(): AbstractStatus<StatusEffectInstantSummon>(
    Name, subscribe: status =>
    {
        status.canSummonMultiple = true;
        status.summonPosition = StatusEffectInstantSummon.Position.Hand;
        status.targetSummon = AbsentUtils.GetStatusOf<StatusEffectSummon>(SummonSarcophagus.Name);
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}