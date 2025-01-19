using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Items;

internal class GoolWrenade() : AbstractItem(
    Name, "Gool Wrenade",
    pools: Pools.None,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(Ethereal.Name, 4),
            AbsentUtils.SStack(WhenDestroyedApplyWeaknessToAllies.Name)
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Wait, this isn't right...";
} 