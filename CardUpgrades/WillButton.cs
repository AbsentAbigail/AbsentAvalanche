using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.CardUpgrades;

internal class WillButton() : AbstractCardUpgrade(
    Name, "Will Button",
    "When destroyed for the first time each fight, gain <1><keyword=scrap> instead\nGain <keyword=immunetosnow>",
    subscribe: charm =>
    {
        charm.effects =
        [
            AbsentUtils.SStack(WhenKilledInsteadGainScrap.Name),
            AbsentUtils.SStack("ImmuneToSnow")
        ];
        charm.targetConstraints = [TargetConstraintHelper.General<TargetConstraintIsUnit>("Is Unit")];
    })
{
    public const string Name = "CardUpgradeWill";
}