using AbsentAvalanche.Patches;
using AbsentAvalanche.StatusEffects;
using AbsentAvalanche.Traits;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

public class Sherba() : AbstractCompanion(Name, "Sherba", 6, 0, 6,
    subscribe: card =>
    {  
        card.attackEffects =
        [
            AbsentUtils.SStack("Snow")
        ];
        card.startWithEffects =
        [
            AbsentUtils.SStack(WhenAllyHitIncreaseEffects.Name)
        ];
        card.traits =
        [
            AbsentUtils.TStack("Barrage"),
            AbsentUtils.TStack(Warm.Name)
        ];
        card.greetMessages =
        [
            "Wants cozy cuddles"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Warm and cozy";
    protected override string BloodProfile => "Blood Profile Snow";
}