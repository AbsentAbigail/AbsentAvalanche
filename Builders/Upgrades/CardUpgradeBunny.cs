using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.Scriptables.CardScripts;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeBunny : IUpgradeBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Absent.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Absent.GetSprite("CardUpgradeBunny"))
            .WithTitle("Bunny Charm")
            .WithText("Swap <keyword=health> and <keyword=attack>")
            .WithPools(CharmPools.GeneralCharms)
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.scripts = [
                    new Script<CardScriptSwapHealthAttack>("Swap Health and Attack", null)
                ];
                charm.targetConstraints =
                [
                    TargetConstraintHelper.HealthMoreThan(0),
                    TargetConstraintHelper.AttackMoreThan(0)
                ];
            });
    }
}