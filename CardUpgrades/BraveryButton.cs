using AbsentAvalanche.CardUpgrades.CardScripts;
using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.CardUpgrades;

internal class BraveryButton() : AbstractCardUpgrade(
    Name, "Bravery Button",
    "When deployed, reduce <keyword=counter> by <1> for each <Companion> on the board\nGain <keyword=pigheaded>",
    subscribe: charm =>
    {
        charm.effects = [AbsentUtils.SStack(WhenDeployedReduceCounterPerAlliedCompanion.Name)];
        charm.giveTraits = [AbsentUtils.TStack("Pigheaded")];
        charm.targetConstraints = [
            TargetConstraintHelper.MaxCounterMoreThan(0),
            TargetConstraintHelper.General<TargetConstraintIsCardType>(
                "Is Not Leader",
                tc => tc.allowedTypes = [AbsentUtils.TryGet<CardType>("Leader")]
            )
        ];
    })
{
    public const string Name = "CardUpgradeBravery";
}