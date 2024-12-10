using AbsentAvalanche.Cards.Clunkers;
using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class InstantTransformIntoBubblesAndCuddlesAscended() : AbstractStatus<StatusEffectInstantChangeForm>(
    Name,
    subscribe: status =>
    {
        var finalBossEffect = AbsentUtils.GetStatusOf<StatusEffectNextPhase>("FinalBossPhase2");
        status.animation = finalBossEffect.animation;
        status.phaseOptions = [AbsentUtils.GetCard(Bubbles.Name), AbsentUtils.GetCard(Cuddles.Name)];
        status.splitCount = 2;
        status.bossTransform = new CardData.StatusEffectStacks(finalBossEffect, 1);
        status.counterChange = -2;
        status.startWithEffects =
        [
            AbsentUtils.SStack("ImmuneToSnow")
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}