using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhenEnemyIsKilledApplyTeethToAttacker() : AbstractApplyXStatus<StatusEffectApplyXWhenUnitIsKilled>(Name)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    
    public override StatusEffectDataBuilder Builder()
    {
        return AbsentUtils.StatusCopy("When Enemy Is Killed Apply Shell To Attacker", Name)
            .WithTextInsert("<{a}><keyword=teeth>")
            .SubscribeToAfterAllBuildEvent(data =>
            {
                var status = (StatusEffectApplyXWhenUnitIsKilled)data;
                status.effectToApply = AbsentUtils.GetStatus("Teeth");
            });
    }
}