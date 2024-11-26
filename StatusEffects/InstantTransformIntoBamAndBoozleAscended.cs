using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class InstantTransformIntoBamAndBoozleAscended() : AbstractStatus<StatusEffectInstantChangeForm>(
    Name,
    subscribe: status =>
    {
        var finalBossEffect = AbsentUtils.GetStatusOf<StatusEffectNextPhase>("FinalBossPhase2");
        status.animation = finalBossEffect.animation;
        status.phaseOptions = [AbsentUtils.GetCard("Bam"), AbsentUtils.GetCard("Boozle")];
        status.splitCount = 2;
        status.bossTransform = new CardData.StatusEffectStacks(finalBossEffect, 1);
        status.healthChange = 30;
        status.damageChange = 1;
        status.counterChange = -2;
        status.startWithEffects =
        [
            AbsentUtils.SStack("ImmuneToSnow")
        ];
    })
{
    public const string Name = "Instant Transform Into Bam and Boozle Ascended";
}