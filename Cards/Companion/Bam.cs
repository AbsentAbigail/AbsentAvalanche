using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

public class Bam() : AbstractCompanion(Name, "Bam", 3, 1, 8,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(WhenAllyHitGainFrenzy.Name)
        ];
        card.greetMessages =
        [
            "I lost my friend, have you seen her? She's a bamboo"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "A friend for a home";
}