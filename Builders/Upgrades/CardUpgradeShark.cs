#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using Calm = AbsentAvalanche.Builders.Keywords.Calm;

#endregion

namespace AbsentAvalanche.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeShark : IUpgradeBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Absent.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Absent.GetSprite("CardUpgradeShark"))
            .WithTitle("Shark Charm")
            .WithText(
                $"Gain <1>{Absent.KeywordTag(Calm.Name)} on kill")
            .WithPools(CharmPools.GeneralCharms)
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.targetConstraints =
                [
                    TargetConstraintHelper.MaxCounterMoreThan(0)
                ];
                charm.effects = [Absent.SStack(OnKillApplyCalmToSelf.Name)];
            });
    }
}