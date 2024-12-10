using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

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
        card.greetMessages =
        [
            "I'm all wound up!"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    protected override string IdleAnimation => "Heartbeat2AnimationProfile";
}