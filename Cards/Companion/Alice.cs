using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

public class Alice() : AbstractCompanion(Name, "Alice", 3, 2, 5,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(WhenEnemyIsKilledGainBlock.Name),
            AbsentUtils.SStack(WhileActiveGainFrenzyEqualToBlock.Name)
        ];
        card.greetMessages =
        [
            "*She moves her head forward expecting headpats*"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "*Cozy paca noises*";
    protected override string BloodProfile => "Blood Profile Snow";
}