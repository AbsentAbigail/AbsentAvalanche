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
public class CardUpgradeEquip : IUpgradeBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Absent.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Absent.GetSprite("CardUpgradeEquip"))
            .WithTitle("Boot Charm")
            .WithText($"Give an item {Absent.KeywordTag(Equip.Name)}")
            .WithPools(CharmPools.GeneralCharms)
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.effects =
                [
                    Absent.SStack(StatusEffects.Equip.Name)
                ];
                charm.scripts =
                [
                    new Script<CardScriptTargetModeBasic>()
                ];
                charm.targetConstraints =
                [
                    TargetConstraintHelper.IsCardType(["Item"]),
                    TargetConstraintHelper.HasStatus(StatusEffects.Equip.Name, not: true),
                    TargetConstraintHelper.HasAttackEffect("Instant Summon Copy Of Item", not: true),
                ];
            });
    }
}