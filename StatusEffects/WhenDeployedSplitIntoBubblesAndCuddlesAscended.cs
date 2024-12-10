using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.Keywords;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhenDeployedSplitIntoBubblesAndCuddlesAscended() : AbstractApplyXStatus<StatusEffectApplyXWhenDeployed>(
    Name, $"{PowerDuo.Tag} evil {AbstractCard.CardTag(Bubbles.Name)} and evil {AbstractCard.CardTag(Cuddles.Name)}",
    effectToApply: InstantTransformIntoBubblesAndCuddlesAscended.Name
)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}