#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeKonami : IUpgradeBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Absent.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Absent.GetSprite("CardUpgradeKonami"))
            .WithTitle("Konami Code")
            .WithText(
                $"Gain {Absent.KeywordTag(Combo.Name)} <1>")
            .WithPools(CharmPools.GeneralCharms)
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.giveTraits = [Absent.TStack(Traits.Combo.Name)];
                charm.targetConstraints = [
                    TargetConstraintHelper.General<TargetConstraintIsCardType>("Is Item", tc => tc.allowedTypes = [Absent.GetCardType("Item")])
                ];
            });
    }
}