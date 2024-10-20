using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Items
{
    internal class Avarice() : AbstractItem(
        Name, "Avarice",
        1,
        subscribe: card =>
        {
            card.startWithEffects = [
                AbsentUtils.SStack(OnHitGainEqualBling.Name),
                AbsentUtils.SStack(HitsAllAlliesAndEnemies.Name),
            ];
        })
    {
        public static string Name => "Avarice";
    }
}