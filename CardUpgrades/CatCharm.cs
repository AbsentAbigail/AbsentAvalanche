using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Cat = AbsentAvalanche.Keywords.Cat;

namespace AbsentAvalanche.CardUpgrades;

internal class CatCharm() : AbstractCardUpgrade(
    Name, "Kids Drawing",
    $"<-2><keyword=attack>\nTrigger: Gain <1>{Cat.Tag}",
    subscribe: charm =>
    {
        charm.damage = -2;
        charm.effects =
        [
            AbsentUtils.SStack(OnCardPlayedGainCat.Name)
        ];
        charm.targetConstraints =
        [
            TargetConstraintHelper.AttackMoreThan(1),
            TargetConstraintHelper.General<TargetConstraintNeedsTarget>("Needs Target")
        ];
    })
{
    public const string Name = "CardUpgradeCat";
}