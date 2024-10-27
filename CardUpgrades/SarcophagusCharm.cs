using AbsentAvalanche.Cards.Items;
using AbsentAvalanche.CardUpgrades.CardScripts;
using AbsentUtilities;

namespace AbsentAvalanche.CardUpgrades;

internal class SarcophagusCharm() : AbstractCardUpgrade(
    Name, "Sarcophagus Charm",
    $"Permanently destroy card when equipped. Gain a {CardHelper.CardTag(Sarcophagus.Name)} holding the destroyed card",
    subscribe: charm =>
    {
        charm.scripts =
        [
            ScriptableHelper.CreateScriptable<CardScriptSarcophagus>(
                "Entomb",
                script => script.vessel = AbsentUtils.GetCard(Sarcophagus.Name)
            )
        ];
    })
{
    public const string Name = "CardUpgradeSarcophagus";
}