using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;

namespace AbsentAvalanche.Cards.Companion;

internal class Eudora() : AbstractCompanion(
    Name, "Eudora",
    4, 1, 8,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack("MultiHit", 2),
            AbsentUtils.SStack("Scrap", 2),
            AbsentUtils.SStack(TriggerWhenAllyBehindTriggers.Name)
        ];
    })
{
    public const string Name = "Eudora";

    public override CardDataBuilder Builder()
    {
        return base.Builder()
            .WithIdleAnimationProfile("Heartbeat2AnimationProfile");
    }
}