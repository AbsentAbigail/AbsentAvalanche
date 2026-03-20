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
public class CardUpgradeWill : IUpgradeBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Absent.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Absent.GetSprite("CardUpgradeWill"))
            .WithTitle("Will Button")
            .WithText(
                "When destroyed for the first time each fight, gain <1><keyword=scrap> instead\nGain <keyword=immunetosnow>")
            .WithPools(CharmPools.GeneralCharms)
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.effects =
                [
                    Absent.SStack(WhenKilledInsteadGainScrap.Name),
                    Absent.SStack("ImmuneToSnow")
                ];
                charm.targetConstraints = [TargetConstraintHelper.General<TargetConstraintIsUnit>("Is Unit")];
            });
    }
}