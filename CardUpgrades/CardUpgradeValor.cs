using AbsentAvalanche.CardUpgrades.CardScripts;
using AbsentAvalanche.Traits;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.CardUpgrades;

internal class CardUpgradeValor() : AbstractCardUpgrade(
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
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}