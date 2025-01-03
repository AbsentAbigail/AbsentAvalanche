﻿using AbsentAvalanche.Cards.Clunkers;
using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.Keywords;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects;

public class WhenDeployedSplitIntoBamAndBoozleAscended() : AbstractApplyXStatus<StatusEffectApplyXWhenDeployed>(
    Name, $"{PowerDuo.Tag} {AbstractCard.CardTag(Bam.Name)} and evil {AbstractCard.CardTag(Boozle.Name)}",
    effectToApply: InstantTransformIntoBamAndBoozleAscended.Name
)
{
    public const string Name = "When Deployed Split Into Bam And Boozle Ascended";
}