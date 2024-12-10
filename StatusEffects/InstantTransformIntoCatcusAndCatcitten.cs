using AbsentAvalanche.Cards.Companion;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class InstantTransformIntoCatcusAndCatcitten() : AbstractStatus<Implementations.StatusEffectInstantChangeForm>(
    Name,
    subscribe: status =>
    {
        status.animation = AbsentUtils.GetStatusOf<StatusEffectNextPhase>("FinalBossPhase2").animation;
        status.phaseOptions = [AbsentUtils.GetCard(Catcus.Name), AbsentUtils.GetCard(Catcitten.Name)];
        status.splitCount = 2;
    })
{
    public const string Name = "InstantTransformIntoCatcusAndCatcitten";
}