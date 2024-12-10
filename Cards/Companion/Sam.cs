using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

public class Sam() : AbstractCompanion(Name, "Sam", 6, 2, 4,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(WhenAllyAheadGainsStatusApplyItToAllies.Name)
        ];
        card.greetMessages =
        [
            "*You're not sure if he's sleeping or not*"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Old dog got tales to tell";
}