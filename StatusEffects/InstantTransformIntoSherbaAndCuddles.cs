using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class InstantTransformIntoSherbaAndCuddles() : AbstractStatus<StatusEffectInstantChangeForm>(
    Name,
    subscribe: status =>
    {
        status.animation = AbsentUtils.GetStatusOf<StatusEffectNextPhase>("FinalBossPhase2").animation;
        status.phaseOptions = [AbsentUtils.GetCard(Sherba.Name), AbsentUtils.GetCard(Cuddles.Name)];
        status.splitCount = 2;
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}