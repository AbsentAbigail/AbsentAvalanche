using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhileActiveItemsHaveCat() : AbstractApplyXStatus<StatusEffectWhileActiveX>(
    Name, "While Active, offensive <Items> have <{a}>" + Keywords.Cat.Tag,
    canBoost: true,
    effectToApply: Cat.Name,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.Hand,
    subscribe: status =>
    {
        status.applyConstraints =
        [
            TargetConstraintHelper.General<TargetConstraintIsItem>("Is Item"),
            TargetConstraintHelper.General<TargetConstraintDoesDamage>("Does Damage")
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}