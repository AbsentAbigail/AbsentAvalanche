using AbsentAvalanche.StatusEffects;
using AbsentAvalanche.Traits;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

public class Cuddles() : AbstractCompanion(Name, "Cuddles", 20, null, 6,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(OnCardPlayedCleanseSelf.Name)
        ];
        card.traits =
        [
            AbsentUtils.TStack(Friend.Name)
        ];
        card.greetMessages =
        [
            "*offers you a hug in these trying times*"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Gives incredible hugs!";
    // protected override string IdleAnimation => "HangAnimationProfile";
}