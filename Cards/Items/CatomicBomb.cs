using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

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
    public const string Name = "CatomicBomb";
}