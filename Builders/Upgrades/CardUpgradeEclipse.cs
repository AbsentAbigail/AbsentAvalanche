#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Helpers;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Builders.Upgrades;

[UsedImplicitly]
public class CardUpgradeEclipse : IUpgradeBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardUpgradeData, CardUpgradeDataBuilder> Builder()
    {
        return new CardUpgradeDataBuilder(Absent.Instance)
            .Create(Name)
            .WithType(CardUpgradeData.Type.Charm)
            .WithImage(Absent.GetSprite("CardUpgradeEclipse"))
            .WithTitle("Eclipse Charm")
            .WithText(
                """
                Add <x1><keyword=frenzy>
                Add <keyword=spark>
                Remove <sprite=counter>
                """)
            .WithPools(CharmPools.GeneralCharms)
            .SubscribeToAfterAllBuildEvent(charm =>
            {
                charm.effects = [Absent.SStack("MultiHit")];
                charm.giveTraits = [Absent.TStack("Spark")];
                charm.counter = 0;
                charm.setCounter = true;
                charm.targetConstraints = [
                    TargetConstraintHelper.MaxCounterMoreThan(0)
                ];
            });
    }
}