using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using Cat = AbsentAvalanche.Builders.Keywords.Cat;

namespace AbsentAvalanche.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeMetronome : IUpgradeBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Absent.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Absent.GetSprite("CardUpgradeMetronome"))
            .WithTitle("Metronome Charm")
            .WithText($"Gain {Absent.KeywordTag(Metronome.Name)}")
            .WithPools(CharmPools.GeneralCharms)
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.giveTraits =
                [
                    Absent.TStack(Traits.Metronome.Name)
                ];
                charm.targetConstraints =
                [
                    TargetConstraintHelper.DoesTrigger(),
                ];
            });
    }
}