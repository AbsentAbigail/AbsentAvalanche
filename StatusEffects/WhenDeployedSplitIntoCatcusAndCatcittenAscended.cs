using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.Keywords;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class WhenDeployedSplitIntoCatcusAndCatcittenAscended() : AbstractApplyXStatus<StatusEffectApplyXWhenDeployed>(
    Name, $"{PowerDuo.Tag} evil {AbstractCard.CardTag(Catcus.Name)} and evil {AbstractCard.CardTag(Catcitten.Name)}",
    effectToApply: InstantTransformIntoCatcusAndCatcittenAscended.Name
)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}