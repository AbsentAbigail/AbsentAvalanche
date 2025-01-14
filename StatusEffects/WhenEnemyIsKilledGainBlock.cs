using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhenEnemyIsKilledGainBlock() : AbstractApplyXStatus<StatusEffectApplyXWhenUnitIsKilled>(
    Name, "When an enemy is killed, gain {0}",
    canBoost: true,
    effectToApply: "Block",
    subscribe: status =>
    {
        status.textInsert = "<{a}><keyword=block>";
        status.enemy = true;
        status.ally = false;
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}