using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

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
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}