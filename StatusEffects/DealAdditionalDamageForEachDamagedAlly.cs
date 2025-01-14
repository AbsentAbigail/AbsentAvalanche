using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

internal class DealAdditionalDamageForEachDamagedAlly() : AbstractStatus<StatusEffectStress>(
    Name, "Deal <{a}> additional damage for each damaged ally",
    canBoost: true,
    subscribe: status => status.on = StatusEffectBonusDamageEqualToX.On.Board
)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}