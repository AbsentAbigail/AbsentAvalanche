using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.CardUpgrades;

internal class FortitudeButton() : AbstractCardUpgrade(
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
    public const string Name = "CardUpgradeFortitude";
}