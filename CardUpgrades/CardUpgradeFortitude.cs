using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.CardUpgrades;

internal class CardUpgradeFortitude() : AbstractCardUpgrade(
    Name, "Fortitude Button",
    "When deployed, gain <2><keyword=health> for each <Companion> on the board",
    
    subscribe: charm =>
    {
        charm.effects = [AbsentUtils.SStack(WhenDeployedGainHealthPerAlliedCompanion.Name, 2)];
        charm.targetConstraints =
        [
            TargetConstraintHelper.HealthMoreThan(0),
            TargetConstraintHelper.General<TargetConstraintIsUnit>("Is Unit")
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}