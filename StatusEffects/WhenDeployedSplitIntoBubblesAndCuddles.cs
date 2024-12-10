using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.Keywords;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhenDeployedSplitIntoBubblesAndCuddles() : AbstractApplyXStatus<StatusEffectApplyXWhenDeployed>(
    Name,
    $"{PowerDuo.Tag} {AbstractCard.CardTag(Bubbles.Name)} and {AbstractCard.CardTag(Cuddles.Name)}",
    canBoost: true,
    effectToApply: InstantTransformIntoBubblesAndCuddles.Name
)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}