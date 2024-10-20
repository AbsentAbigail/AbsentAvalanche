using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Cards.Companion;

internal class SalvoKitty() : AbstractUnit(
    Name, "Salvo Kitty",
    4, null, 3,
    Pools.None,
    card =>
    {
        card.AddToPets();
        card.startWithEffects =
        [
            AbsentUtils.SStack(OnCardPlayedAddMissileToHand.Name, 2)
        ];
    })
{
    public const string Name = "SalvoKitty";
}