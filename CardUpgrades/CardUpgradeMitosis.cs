using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.CardUpgrades;

internal class CardUpgradeMitosis() : AbstractCardUpgrade(
    Name, "Mitosis Charm",
    "<keyword=split> when <4><keyword=health> lost",
    subscribe: charm =>
    {
        charm.effects =
        [
            AbsentUtils.SStack("When X Health Lost Split", 4)
        ];

        charm.targetConstraints =
        [
            TargetConstraintHelper.HealthMoreThan(0)
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}