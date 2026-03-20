#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.Scriptables.CardScripts;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeViolence : IUpgradeBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Absent.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Absent.GetSprite("CardUpgradeViolence"))
            .WithTitle("Violence Charm")
            .WithText(
                "<+1><keyword=attack>, then double current <keyword=attack>\nSet <keyword=health> to <1>\nGain <keyword=fragile>")
            .WithPools(CharmPools.GeneralCharms)
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.setHp = true;
                charm.hp = 1;
                charm.damage = 1;

                charm.scripts =
                [
                    new Script<CardScriptMultiplyDamage>(
                        "Double Damage",
                        script => script.multiply = 2
                    )
                ];

                charm.giveTraits = [Absent.TStack("Fragile")];
            });
    }
}