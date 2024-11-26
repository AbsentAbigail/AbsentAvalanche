using AbsentAvalanche.StatusEffects;
using AbsentAvalanche.Traits;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Items;

internal class GhostlyPresence() : AbstractItem(
    Name, "Ghostly Presence",
    pools: Pools.Shademancer,
    subscribe: card =>
    {
        card.traits = [AbsentUtils.TStack(Rest.Name, 3)];
        card.startWithEffects =
        [
            AbsentUtils.SStack(Ethereal.Name, 3),
            AbsentUtils.SStack(WhileInHandApplyOverburnToRandomEnemy.Name)
        ];
    })
{
    public const string Name = "GhostlyPresence";
}