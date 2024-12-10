using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class InstantTransformIntoCatcusAndCatcittenAscended() : AbstractStatus<StatusEffectInstantChangeForm>(
    Name,
    subscribe: status =>
    {
        var finalBossEffect = AbsentUtils.GetStatusOf<StatusEffectNextPhase>("FinalBossPhase2");
        status.animation = finalBossEffect.animation;
        status.phaseOptions = [AbsentUtils.GetCard(Catcus.Name), AbsentUtils.GetCard(Catcitten.Name)];
        status.splitCount = 2;
        status.bossTransform = new CardData.StatusEffectStacks(finalBossEffect, 1);
        status.startWithEffects =
        [
            AbsentUtils.SStack("ImmuneToSnow"),
            AbsentUtils.SStack(Cat.Name, 4)
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}