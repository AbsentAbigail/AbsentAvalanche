using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Items;

public class Blanket() : AbstractItem(Name, "Blanket",
    needsTarget: true, pools: Pools.None, subscribe: card =>
    {
        card.attackEffects =
        [
            AbsentUtils.SStack("Spice"),
            AbsentUtils.SStack("Shell"),
            AbsentUtils.SStack(Calm.Name)
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Become a cozy burrito";
}