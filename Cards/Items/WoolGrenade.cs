using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Items;

internal class WoolGrenade() : AbstractItem(
    Name, "Wool Grenade",
    0, true,
    pools: Pools.None,
    subscribe: card =>
    {
        card.attackEffects = [AbsentUtils.SStack("Weakness")];
        card.startWithEffects =
        [
            AbsentUtils.SStack(Ethereal.Name, 4),
            AbsentUtils.SStack(WhenDestroyedApplyWeaknessToEnemies.Name, 3)
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
} 