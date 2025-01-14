using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Items;

public class Catbom() : AbstractItem(
    Name, "Bomeow",
    0, true,
    Pools.Clunkmaster,
    subscribe: card =>
    {
        card.attackEffects =
        [
            AbsentUtils.SStack("Weakness", 4),
            AbsentUtils.SStack(Cat.Name, 8),
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}