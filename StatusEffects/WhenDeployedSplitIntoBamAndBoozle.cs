using AbsentAvalanche.Cards.Clunkers;
using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.Keywords;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class WhenDeployedSplitIntoBamAndBoozle() : AbstractApplyXStatus<StatusEffectApplyXWhenDeployed>(
    Name, $"{PowerDuo.Tag} {AbstractCard.CardTag(Bam.Name)} and {AbstractCard.CardTag(Boozle.Name)}",
    canBoost: true,
    effectToApply: InstantTransformIntoBamAndBoozle.Name
)
{
    public const string Name = "When Deployed Split Into Bam And Boozle";
}