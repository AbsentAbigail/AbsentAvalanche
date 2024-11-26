using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

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
    public const string Name = "Pillow";
}