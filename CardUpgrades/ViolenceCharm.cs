using AbsentAvalanche.CardUpgrades.CardScripts;
using AbsentUtilities;

namespace AbsentAvalanche.CardUpgrades;

internal class ViolenceCharm() : AbstractCardUpgrade(
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
    public const string Name = "CardUpgradeViolence";
}