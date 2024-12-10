using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.Keywords;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class WhenDeployedSplitIntoCatcusAndCatcitten() : AbstractApplyXStatus<StatusEffectApplyXWhenDeployed>(
    Name, $"{PowerDuo.Tag} {AbstractCard.CardTag(Catcus.Name)} and {AbstractCard.CardTag(Catcitten.Name)}",
    canBoost: true,
    effectToApply: InstantTransformIntoCatcusAndCatcitten.Name
)
{
    public const string Name = "WhenDeployedSplitIntoCatcusAndCatcitten";
}