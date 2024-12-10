using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

public class Seal() : AbstractCompanion(Name, "Seal", 8, 8, 4,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(DoubleStatusEffectsAppliedToCatcus.Name),
            AbsentUtils.SStack("ImmuneToSnow")
        ];
        card.greetMessages =
        [
            "*rolls around blissfully*"
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Friends support each other";
    protected override string BloodProfile => "Blood Profile Snow";
    protected override string IdleAnimation => "GiantAnimationProfile";
}