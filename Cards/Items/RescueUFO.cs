using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Items;

internal class RescueUFO() : AbstractItem(
    Name, "Rescue UFO",
    needsTarget: true,
    shopPrice: 40,
    playOnHand: false,
    subscribe: card =>
    {
        card.attackEffects = [AbsentUtils.SStack(Abduct.Name)];
        card.traits =
        [
            AbsentUtils.TStack("Consume"),
            AbsentUtils.TStack("Zoomlin")
        ];
    })
{
    public const string Name = "RescueUFO";
}