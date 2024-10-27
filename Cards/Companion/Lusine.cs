using AbsentAvalanche.Traits;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Cards.Companion;

internal class Lusine() : AbstractCompanion(
    Name, "Lusine",
    8, 2, 4,
    subscribe: card =>
    {
        card.AttackEffects(
            new Stack("Demonize", 1));
        card.startWithEffects =
        [
            AbsentUtils.SStack("MultiHit")
        ];
        card.traits =
        [
            AbsentUtils.TStack(Trample.Name)
        ];
    })
{
    public const string Name = "Lusine";

    public override CardDataBuilder Builder()
    {
        return base.Builder()
            .WithIdleAnimationProfile("PulseAnimationProfile");
    }
}