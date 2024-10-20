using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Items;

internal class CursedClaymore() : AbstractItem(
    Name, "Cursed Claymore",
    10, true,
    subscribe: card =>
    {
        card.traits = [];
        card.StartWithEffects(
            new Stack(Ethereal.Name, 1),
            new Stack(WhenDestroyedDealDamageToRandomAlly.Name, 3)
        );
    })
{
    public const string Name = "CursedClaymore";
}