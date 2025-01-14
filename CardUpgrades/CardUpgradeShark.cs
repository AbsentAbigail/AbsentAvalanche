using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.CardUpgrades;

internal class CardUpgradeShark() : AbstractCardUpgrade(
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
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}