using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhileActiveCountDownEtherealWhenDrawn() : AbstractApplyXStatus<StatusEffectWhileActiveX>(
    Name, $"While active, cards count down {Keywords.Ethereal.Tag} by <{{a}}> when drawn",
    canBoost: true,
    effectToApply: CountDownEtherealWhenDrawn.Name,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.Hand,
    subscribe: status => status.applyConstraints = [TargetConstraintHelper.HasStatus(Ethereal.Name)])
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}