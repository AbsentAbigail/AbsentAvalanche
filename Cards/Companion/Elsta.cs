using AbsentAvalanche.Traits;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

internal class Elsta() : AbstractCompanion(
    Name, "Elsta",
    5, 1, 4,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack("On Kill Apply Gold To Self", 4),
            AbsentUtils.SStack("MultiHit")
        ];
        card.traits =
        [
            AbsentUtils.TStack(GoldRush.Name)
        ];
        card.greetMessages =
        [
            "*stares at your Bling pouch*"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Ooo shiny!";
    protected override string IdleAnimation => "Heartbeat2AnimationProfile";
}