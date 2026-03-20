#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using Cat = AbsentAvalanche.Builders.Keywords.Cat;

#endregion

namespace AbsentAvalanche.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeCat : IUpgradeBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Absent.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Absent.GetSprite("CardUpgradeCat"))
            .WithTitle("Kids Drawing")
            .WithText($"<-2><keyword=attack>\nTrigger: Gain <1>{Absent.KeywordTag(Cat.Name)}")
            .WithPools(CharmPools.GeneralCharms)
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.damage = -2;
                charm.effects =
                [
                    Absent.SStack(OnCardPlayedGainCat.Name)
                ];
                charm.targetConstraints =
                [
                    TargetConstraintHelper.AttackMoreThan(1),
                    TargetConstraintHelper.DoesTrigger(),
                    TargetConstraintHelper.General<TargetConstraintPlayOnSlot>(
                        "Does Not Play On Slot",
                        tc => tc.slot = true,
                        not: true
                    ),
                    TargetConstraintHelper.General<TargetConstraintPlayOnSlot>(
                        "Plays On Board",
                        tc => tc.board = true
                    )
                ];
            });
    }
}