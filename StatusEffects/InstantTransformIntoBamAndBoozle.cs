using AbsentAvalanche.Cards.Clunkers;
using AbsentAvalanche.Cards.Companion;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class InstantTransformIntoBamAndBoozle() : AbstractStatus<Implementations.StatusEffectInstantChangeForm>(
    Name,
    subscribe: status =>
    {
        status.animation = AbsentUtils.GetStatusOf<StatusEffectNextPhase>("FinalBossPhase2").animation;
        status.phaseOptions = [AbsentUtils.GetCard(Bam.Name), AbsentUtils.GetCard(Boozle.Name)];
        status.splitCount = 2;
    })
{
    public const string Name = "Instant Transform Into Bam and Boozle";
}