using AbsentAvalanche.CardUpgrades.CardScripts;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.CardUpgrades;

public class CardUpgradeChangeLeader() : AbstractCardUpgrade(
    Name, "Inheritance Charm",
    "Demote <Leader> into a Companion and promote the leftmost active Companion into a Leader",
    subscribe: charm =>
    {
        charm.scripts =
        [
            ScriptableHelper.CreateScriptable<CardScriptChangeLeader>("Change Leader Script")
        ];
        charm.targetConstraints =
        [
            TargetConstraintHelper.General<TargetConstraintIsCardType>("Is Leader",
                tc => tc.allowedTypes = [AbsentUtils.GetCardType("Leader")]),
            TargetConstraintHelper.General<TargetConstraintCompanionInDeck>("Companion In Deck")
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}