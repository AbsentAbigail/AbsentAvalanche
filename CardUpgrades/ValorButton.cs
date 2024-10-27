using AbsentAvalanche.CardUpgrades.CardScripts;
using AbsentAvalanche.Traits;
using AbsentUtilities;

namespace AbsentAvalanche.CardUpgrades;

internal class ValorButton() : AbstractCardUpgrade(
    Name, "Valour Button",
    $"Double current <keyword=attack>\nGain {Keywords.Valor.Tag}",
    subscribe: charm =>
    {
        charm.targetConstraints =
        [
            TargetConstraintHelper.General<TargetConstraintDoesAttack>("Does attack"),
            TargetConstraintHelper.HasTrait("Barrage", not: true),
            TargetConstraintHelper.HasTrait("Aimless", not: true),
            TargetConstraintHelper.HasTrait("Longshot", not: true)
        ];
        charm.giveTraits = [AbsentUtils.TStack(Valor.Name)];

        charm.scripts =
        [
            ScriptableHelper.CreateScriptable<CardScriptMultiplyDamage>(
                "Double Damage",
                script => script.multiply = 2
            )
        ];
    })
{
    public const string Name = "CardUpgradeValor";
}