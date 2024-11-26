using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class InstantTransformIntoBamAndBoozle() : AbstractStatus<Implementations.StatusEffectInstantChangeForm>(
    Name,
    subscribe: status =>
    {
        status.animation = AbsentUtils.GetStatusOf<StatusEffectNextPhase>("FinalBossPhase2").animation;
        status.phaseOptions = [AbsentUtils.GetCard("Bam"), AbsentUtils.GetCard("Boozle")];
        status.splitCount = 2;
    })
{
    public const string Name = "Instant Transform Into Bam and Boozle";
}