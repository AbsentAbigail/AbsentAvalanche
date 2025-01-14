using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Items;

public class Headpat() : AbstractItem(Name, "Headpat",
    needsTarget: true, pools: Pools.None, playOnHand: false, subscribe: card =>
    {
        card.attackEffects =
        [
            AbsentUtils.SStack("Heal", 2),
            AbsentUtils.SStack(InstantCleanseText.Name),
            AbsentUtils.SStack(InstantHeadpat.Name)
        ];
        card.traits = [AbsentUtils.TStack("Draw")];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "There there, you did well!";
}