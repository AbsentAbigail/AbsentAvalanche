#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.Scriptables.CardScripts;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeValor : IUpgradeBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Absent.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Absent.GetSprite("CardUpgradeValor"))
            .WithTitle("Valour Button")
            .WithText(
                $"Double current <keyword=attack>\nGain {Absent.KeywordTag(Valor.Name)}")
            .WithPools(CharmPools.GeneralCharms)
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.targetConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintDoesDamage>("Does damage"),
                    TargetConstraintHelper.HasTrait("Barrage", not: true),
                    TargetConstraintHelper.HasTrait("Aimless", not: true),
                    TargetConstraintHelper.HasTrait("Longshot", not: true)
                ];
                charm.giveTraits = [Absent.TStack(Traits.Valor.Name)];

                charm.scripts =
                [
                    new Script<CardScriptMultiplyDamage>(
                        "Double Damage",
                        script => script.multiply = 2
                    )
                ];
            });
    }
}