using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Items;

internal class CursedClaymore() : AbstractItem(
    Name, "Cursed Claymore",
    10, true,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(Ethereal.Name),
            AbsentUtils.SStack(WhenDestroyedDealDamageToRandomAlly.Name)
        ];
    })
{
    public const string Name = "CursedClaymore";
}