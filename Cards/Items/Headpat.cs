using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

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
    public const string Name = "Headpat";
    public override string FlavourText => "There there, you did well!";
}