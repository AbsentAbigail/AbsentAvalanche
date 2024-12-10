using AbsentAvalanche.Traits;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

internal class Lusine() : AbstractCompanion(
    Name, "Lusine",
    8, 2, 4,
    subscribe: card =>
    {
        card.attackEffects =
        [
            AbsentUtils.SStack("Demonize")
        ];
        card.startWithEffects =
        [
            AbsentUtils.SStack("MultiHit")
        ];
        card.traits =
        [
            AbsentUtils.TStack(Trample.Name)
        ];
        card.greetMessages =
        [
            "Don't stand in my way, or I'll have to step on you"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    protected override string IdleAnimation => "PulseAnimationProfile";
}