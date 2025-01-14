using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

public class April() : AbstractCompanion(Name, "April", 4, 0, 4,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(OnCardPlayedAddWoolGrenadeToHand.Name)
        ];
        card.greetMessages =
        [
            "Is it time to throw soft explosives?"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Woolly Friend";
}