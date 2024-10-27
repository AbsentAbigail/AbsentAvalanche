using AbsentAvalanche.CardUpgrades.CardScripts;
using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.CardUpgrades;

internal class BraveryButton() : AbstractCardUpgrade(
    Name, "Bravery Button",
    "When deployed, reduce <keyword=counter> by <1> for each allied companion\nGain <keyword=pigheaded>",
    subscribe: charm =>
    {
        charm.effects = [AbsentUtils.SStack(WhenDeployedReduceCounterPerAlliedCompanion.Name)];
        charm.giveTraits = [AbsentUtils.TStack("Pigheaded")];
        charm.targetConstraints = [TargetConstraintHelper.MaxCounterMoreThan(0)];
    })
{
    public const string Name = "CardUpgradeBravery";
}