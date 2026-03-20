#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeFortitude : IUpgradeBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Absent.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Absent.GetSprite("CardUpgradeFortitude"))
            .WithTitle("Fortitude Button")
            .WithText(
                """
                When deployed, gain <2><keyword=health> for each <Companion> on the board
                """)
            .WithPools(CharmPools.GeneralCharms)
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.effects = [Absent.SStack(WhenDeployedGainHealthPerAlliedCompanion.Name, 2)];
                charm.targetConstraints =
                [
                    TargetConstraintHelper.HealthMoreThan(0),
                    TargetConstraintHelper.General<TargetConstraintIsUnit>("Is Unit")
                ];
            });
    }
}