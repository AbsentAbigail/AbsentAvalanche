using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

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
    public const string Name = "Catbom";
}