using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Items;

public class CatomicBomb() : AbstractItem(
    Name, "Catomic Bomb",
    0,
    subscribe: card =>
    {
        card.attackEffects =
        [
            AbsentUtils.SStack(Cat.Name, 2)
        ];
        card.startWithEffects =
        [
            AbsentUtils.SStack(Cat.Name),
            AbsentUtils.SStack(OnCardPlayedDoubleAllCat.Name),
            AbsentUtils.SStack(HitsAllAlliesAndEnemies.Name)
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}