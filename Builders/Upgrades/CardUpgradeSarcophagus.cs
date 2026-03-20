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
public class CardUpgradeSarcophagus : IUpgradeBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Absent.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Absent.GetSprite("CardUpgradeSarcophagus"))
            .WithTitle("Sarcophagus Charm")
            .WithText(
                $"Permanently <seal> a card inside a {Absent.KeywordTag(Sarcophagus.Name)}")
            .WithPools(CharmPools.GeneralCharms)
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.scripts =
                [
                    new Script<CardScriptSarcophagus>(
                        "Entomb",
                        script => script.vessel = Absent.GetCard(Cards.Items.Sarcophagus.Name)
                    )
                ];
                charm.targetConstraints =
                [
                    TargetConstraintHelper.General<TargetConstraintIsCardType>(
                        "Is Not Leader",
                        modification: tc => tc.allowedTypes = [Absent.GetCardType("Leader")],
                        not: true
                    ),
                    TargetConstraintHelper.General<TargetConstraintIsInDeck>("Is In Deck")
                ];
                charm.takeSlot = false;
            });
    }
}