using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Items;

internal class Avarice() : AbstractItem(
    Name, "Avarice",
    1,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(OnHitGainEqualBling.Name),
            AbsentUtils.SStack(HitsAllAlliesAndEnemies.Name)
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}