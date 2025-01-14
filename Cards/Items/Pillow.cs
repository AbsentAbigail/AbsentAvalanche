using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Items;

public class Pillow() : AbstractItem(Name, "Pillow",
    needsTarget: true, pools: Pools.None, playOnHand: false, subscribe: card =>
    {
        card.attackEffects =
        [
            AbsentUtils.SStack("Heal", 2)
        ];
        card.traits = [
            AbsentUtils.TStack("Zoomlin"),
            AbsentUtils.TStack("Consume")
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}