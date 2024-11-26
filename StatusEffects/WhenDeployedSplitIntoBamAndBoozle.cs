using AbsentAvalanche.Cards.Clunkers;
using AbsentAvalanche.Cards.Companion;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class WhenDeployedSplitIntoBamAndBoozle() : AbstractApplyXStatus<StatusEffectApplyXWhenDeployed>(
    Name, $"When deployed, split into {AbstractCard.CardTag(Bam.Name)} and {AbstractCard.CardTag(Boozle.Name)}",
    effectToApply: InstantTransformIntoBamAndBoozle.Name
)
{
    public const string Name = "When Deployed Split Into Bam And Boozle";
}