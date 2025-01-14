using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

public class Nami() : AbstractCompanion(Name, "Nami", 7, 2, 4,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(OnCardPlayedApplyRandomBuffToRandomAlly.Name)
        ];
        card.greetMessages =
        [
            "*She starts cuddling you*"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "*Rainbow paca noises*";
    protected override string BloodProfile => "Blood Profile Pink Wisp";
}