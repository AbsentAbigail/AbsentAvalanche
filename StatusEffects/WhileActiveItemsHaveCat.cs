using AbsentUtilities;

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
    public const string Name = "While Active Items Have Cat";
}