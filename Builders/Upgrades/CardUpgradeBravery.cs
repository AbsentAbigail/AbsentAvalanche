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
public class CardUpgradeBravery : IUpgradeBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Absent.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Absent.GetSprite("CardUpgradeBravery"))
            .WithTitle("Bravery Button")
            .WithText("When deployed, reduce <keyword=counter> by <1> for each <Companion> on the board\nGain <keyword=pigheaded>")
            .WithPools(CharmPools.GeneralCharms)
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.effects = [Absent.SStack(WhenDeployedReduceCounterPerAlliedCompanion.Name)];
                charm.giveTraits = [Absent.TStack("Pigheaded")];
                charm.targetConstraints =
                [
                    TargetConstraintHelper.MaxCounterMoreThan(0),
                    TargetConstraintHelper.General<TargetConstraintIsCardType>(
                        "Is Not Leader",
                        tc => tc.allowedTypes = [Absent.GetCardType("Leader")],
                        true
                    )
                ];
            });
    }
}