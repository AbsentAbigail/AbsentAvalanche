using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.StatusEffects;

public class DoubleStatusEffectsAppliedToCatcus() : AbstractApplyXStatus<StatusEffectApplyXWhenPositiveYAppliedTo>(
    Name, "Double positive Status Effects applied to {0}",
    effectToApply: null,
    applyToFlags: StatusEffectApplyX.ApplyToFlags.Allies,
    subscribe: status =>
    {
        status.textInsert = AbstractCard.CardTag(Catcus.Name);
        status.whenAppliedToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
        status.whenAnyApplied = true;
        status.adjustAmount = true;
        status.multiplyAmount = 2F;
        status.applyConstraints = [TargetConstraintHelper.General<TargetConstraintIsSpecificCard>(
                "Is Catcus",
                tc => tc.allowedCards = [
                    AbsentUtils.GetCard(Catcus.Name),
                    AbsentUtils.GetCard(Catcitten.Name)
                ]
            )];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}