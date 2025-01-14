using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class InstantRandomBuff() : AbstractStatus<StatusEffectInstantApplyRandom>(
    Name, "Random Buff", canBoost: true,
    subscribe: status =>
    {
        status.applyFormatKey = AbsentUtils.GetStatus("Shroom").applyFormatKey;
        
        status.possibleEffects =
        [
            AbsentUtils.GetStatus(Cat.Name),
            AbsentUtils.GetStatus("Block"),
            AbsentUtils.GetStatus("MultiHit"),
            AbsentUtils.GetStatus("Shell"),
            AbsentUtils.GetStatus("Spice"),
            AbsentUtils.GetStatus("Teeth"),
            AbsentUtils.GetStatus("Reduce Counter"),
            AbsentUtils.GetStatus("Reduce Max Counter")
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}