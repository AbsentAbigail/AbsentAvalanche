using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Items;

internal class Missile() : AbstractItem(
    Name, "Missile",
    1, true,
    Pools.None,
    subscribe: card =>
    {
        card.traits = [AbsentUtils.TStack("Consume")];
        card.startWithEffects =
        [
            AbsentUtils.SStack(TriggerAgainstTargetWhenMissileAttacks.Name)
        ];
    })
{
    public const string Name = "Missile";
}