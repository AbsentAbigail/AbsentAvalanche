using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.CardUpgrades;

internal class SharkCharm() : AbstractCardUpgrade(
    Name, "Shark Charm",
    $"Gain <1>{Keywords.Calm.Tag} on kill",
    subscribe: charm =>
    {
        charm.targetConstraints =
        [
            TargetConstraintHelper.MaxCounterMoreThan(0)
        ];
        charm.effects = [AbsentUtils.SStack(OnKillApplyCalmToSelf.Name)];
    })
{
    public const string Name = "CardUpgradeShark";
}