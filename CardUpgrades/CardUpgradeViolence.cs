using AbsentAvalanche.CardUpgrades.CardScripts;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.CardUpgrades;

internal class CardUpgradeViolence() : AbstractCardUpgrade(
    Name, "Violence Charm",
    "<+1><keyword=attack>, then double current <keyword=attack>\nSet <keyword=health> to <1>\nGain <keyword=fragile>",
    subscribe: charm =>
    {
        charm.setHp = true;
        charm.hp = 1;
        charm.damage = 1;

        charm.scripts =
        [
            ScriptableHelper.CreateScriptable<CardScriptMultiplyDamage>(
                "Double Damage",
                script => script.multiply = 2
            )
        ];

        charm.giveTraits = [AbsentUtils.TStack("Fragile")];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}