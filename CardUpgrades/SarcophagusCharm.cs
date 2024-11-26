using AbsentAvalanche.CardUpgrades.CardScripts;
using AbsentAvalanche.Keywords;
using AbsentUtilities;

namespace AbsentAvalanche.CardUpgrades;

internal class SarcophagusCharm() : AbstractCardUpgrade(
    Name, "Sarcophagus Charm",
    $"Permanently <seal> a card inside a {Sarcophagus.Tag}",
    subscribe: charm =>
    {
        charm.scripts =
        [
            ScriptableHelper.CreateScriptable<CardScriptSarcophagus>(
                "Entomb",
                script => script.vessel = AbsentUtils.GetCard(Cards.Items.Sarcophagus.Name)
            )
        ];
        charm.targetConstraints =
        [
            TargetConstraintHelper.General<TargetConstraintIsCardType>(
                "Is Not Leader",
                modification: tc => tc.allowedTypes = [AbsentUtils.TryGet<CardType>("Leader")],
                not: true
            )
        ];
        charm.takeSlot = false;
    })
{
    public const string Name = "CardUpgradeSarcophagus";
}