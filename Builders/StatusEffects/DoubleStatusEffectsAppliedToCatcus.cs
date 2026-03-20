#region

using AbsentAvalanche.Builders.Cards.Companions;
using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class DoubleStatusEffectsAppliedToCatcus : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectApplyXWhenPositiveYAppliedTo>(Name)
            .WithText("Double positive Status Effects applied to {0}")
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectApplyXWhenPositiveYAppliedTo>(status =>
            {
                status.textInsert = Absent.CardTag(Catcus.Name);
                status.whenAppliedToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
                status.whenAnyApplied = true;
                status.adjustAmount = true;
                status.multiplyAmount = 2F;
                status.applyConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintIsSpecificCard>(
                        "Is Catcus",
                        tc => tc.allowedCards =
                        [
                            Absent.GetCard(Catcus.Name),
                            Absent.GetCard(Catcitten.Name)
                        ]
                    )
                ];
                status.applyToFlags = StatusEffectApplyX.ApplyToFlags.Allies;
            });
    }
}