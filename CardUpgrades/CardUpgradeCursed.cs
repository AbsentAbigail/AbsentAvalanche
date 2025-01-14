using AbsentAvalanche.Keywords;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.CardUpgrades;

internal class CardUpgradeCursed() : AbstractCardUpgrade(
    Name, "Sacrificial Dagger Charm",
    $"<+3><keyword=attack>\nGain <2>{Ethereal.Tag}",
    subscribe: charm =>
    {
        charm.damage = 3;

        charm.effects = [AbsentUtils.SStack(StatusEffects.Ethereal.Name, 2)];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}