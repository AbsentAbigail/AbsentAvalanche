using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.Keywords;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhenDeployedSplitIntoSherbaAndCuddlesAscended() : AbstractApplyXStatus<StatusEffectApplyXWhenDeployed>(
    Name, $"{PowerDuo.Tag} evil {AbstractCard.CardTag(Sherba.Name)} and evil {AbstractCard.CardTag(Cuddles.Name)}",
    effectToApply: InstantTransformIntoSherbaAndCuddlesAscended.Name
)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}