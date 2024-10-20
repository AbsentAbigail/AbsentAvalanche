using AbsentAvalanche.Keywords;
using AbsentUtilities;

namespace AbsentAvalanche.CardUpgrades;

internal class CursedCharm() : AbstractCardUpgrade(
    Name, "Cursed Charm",
    $"<+3><keyword=attack>\nGain <2>{Ethereal.Tag}",
    subscribe: charm =>
    {
        charm.damage = 3;

        charm.effects = [AbsentUtils.SStack(StatusEffects.Ethereal.Name, 2)];
    })
{
    public const string Name = "CardUpgradeCursed";
}