using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

public class Bubbles() : AbstractCompanion(Name, "Bubbles", 4,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(OnCardPlayedGainSnow.Name, 2),
            AbsentUtils.SStack(OnCardPlayedTriggerAllyAhead.Name),
            AbsentUtils.SStack("Trigger When Redraw Hit")
        ];
        card.greetMessages =
        [
            "She's cheering you on!"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Smol bear, big world";
    protected override string BloodProfile => "Blood Profile Snow";
}