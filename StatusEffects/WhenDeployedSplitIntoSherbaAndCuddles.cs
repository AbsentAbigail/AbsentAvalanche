using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.Keywords;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhenDeployedSplitIntoSherbaAndCuddles() : AbstractApplyXStatus<StatusEffectApplyXWhenDeployed>(
    Name,
    $"{PowerDuo.Tag} {AbstractCard.CardTag(Sherba.Name)} and {AbstractCard.CardTag(Cuddles.Name)}",
    canBoost: true,
    effectToApply: InstantTransformIntoSherbaAndCuddles.Name
)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}