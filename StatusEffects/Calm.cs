using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using WildfrostHopeMod.VFX;

namespace AbsentAvalanche.StatusEffects;

internal class Calm() : AbstractApplyXStatus<StatusEffectCalm>(
    Name,
    effectToApply: "Reduce Max Counter",
    subscribe: status =>
    {
        status.countDownEffect = AbsentUtils.GetStatus("Reduce Counter");
        status.counterIncreaseEffect = AbsentUtils.GetStatus("Increase Max Counter");

        status.applyEqualAmount = true;
        status.eventPriority = -10;

        status.targetConstraints = [TargetConstraintHelper.MaxCounterMoreThan(0)];
    })
{
    public const string Name = "Calm";

    public override StatusEffectDataBuilder Builder()
    {
        return base.Builder()
            .WithTextInsert("{a}")
            .WithIcon_VFX("calm", "calm", Keywords.Calm.NameWithGuid,
                VFXMod_StatusEffectHelpers.LayoutGroup.counter);
    }
}