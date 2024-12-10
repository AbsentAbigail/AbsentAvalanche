using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

public class Chirp() : AbstractCompanion(Name, "Chirp", 3, 3, 3,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(OnCardPlayedDrawAndApplyFrenzyAndAimless.Name)
        ];
        card.greetMessages =
        [
            "*chirps along shily*"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "*chirp*";
    protected override string IdleAnimation => "HangAnimationProfile";
}