using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

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
    public const string Name = "Blanket";
    public override string FlavourText => "Become a cozy burrito";
}