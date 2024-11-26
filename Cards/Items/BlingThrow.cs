using AbsentAvalanche.Traits;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Items;

internal class BlingThrow() : AbstractItem(
    Name, "Bling Throw",
    5, true,
    playOnHand: false,
    subscribe: card =>
    {
        card.traits = [AbsentUtils.TStack(GoldRush.Name)];
        card.startWithEffects =
        [
            AbsentUtils.SStack("Pre Turn Take Gold", 2),
            AbsentUtils.SStack("On Kill Apply Gold To Self", 2)
        ];
    })
{
    public const string Name = "BlingThrow";
}