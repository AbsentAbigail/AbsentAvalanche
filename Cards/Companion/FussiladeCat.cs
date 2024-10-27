using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Cards.Companion;

internal class FussiladeCat() : AbstractCompanion(
    Name, "Fussilade Cat",
    9, null, 3,
    Pools.None,
    card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack("Weakness"),
            AbsentUtils.SStack(WhenEnemyIsHitByItemApplyWeaknessToThem.Name),
            AbsentUtils.SStack(WhileActiveItemsHaveCat.Name),
            AbsentUtils.SStack(OnCardPlayedAddMissileToHand.Name, 3),
        ];
    })
{
    public const string Name = "FussiladeCat";
}