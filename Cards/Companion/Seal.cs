using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Companion;

public class Seal() : AbstractCompanion(Name, "Seal", 8, 8, 4,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(DoubleStatusEffectsAppliedToCatcus.Name),
            AbsentUtils.SStack("ImmuneToSnow")
        ];
    })
{
    public const string Name = "Seal";
    public override string FlavourText => "Friends support each other";
    protected override string BloodProfile => "Blood Profile Snow";
    protected override string IdleAnimation => "GiantAnimationProfile";
}