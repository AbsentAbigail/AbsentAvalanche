using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.CardUpgrades;

internal class CardUpgradeBravery() : AbstractCardUpgrade(
    Name, "Bravery Button",
    "When deployed, reduce <keyword=counter> by <1> for each <Companion> on the board\nGain <keyword=pigheaded>",
    subscribe: charm =>
    {
        charm.effects = [AbsentUtils.SStack(WhenDeployedReduceCounterPerAlliedCompanion.Name)];
        charm.giveTraits = [AbsentUtils.TStack("Pigheaded")];
        charm.targetConstraints =
        [
            TargetConstraintHelper.MaxCounterMoreThan(0),
            TargetConstraintHelper.General<TargetConstraintIsCardType>(
                "Is Not Leader",
                tc => tc.allowedTypes = [AbsentUtils.TryGet<CardType>("Leader")],
                true
            )
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}