using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Cards.Companion;

internal class SalvoKitty() : AbstractCompanion(
    Name, "Salvo Kitty",
    4, null, 3,
    Pools.None,
    card =>
    {
        card.AddToPets();
        card.startWithEffects =
        [
            AbsentUtils.SStack(WhileActiveMissilesHaveCat.Name),
            AbsentUtils.SStack(OnCardPlayedAddMissileToHand.Name, 2),
        ];
    })
{
    public const string Name = "SalvoKitty";
}