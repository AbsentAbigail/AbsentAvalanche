#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.Scriptables.CardScripts;
using AbsentAvalanche.Scriptables.TargetConstraints;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeChangeLeader : IUpgradeBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Absent.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Absent.GetSprite("CardUpgradeChangeLeader"))
            .WithTitle("Inheritance Charm")
            .WithText("Demote <Leader> into a Companion and promote the leftmost active Companion into a Leader")
            .WithPools(CharmPools.GeneralCharms)
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.scripts =
                [
                    new Script<CardScriptChangeLeader>("Change Leader Script", null)
                ];
                charm.targetConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintIsCardType>("Is Leader",
                        tc => tc.allowedTypes = [Absent.GetCardType("Leader")]),
                    TargetConstraintHelper.General<TargetConstraintCompanionInDeck>("Companion In Deck")
                ];
            });
    }
}