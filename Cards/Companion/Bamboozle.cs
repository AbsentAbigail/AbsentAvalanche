using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

public class Bamboozle() : AbstractCompanion(Name, "Bam and Boozle", 3, 1, 8,
    pools: Pools.None,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack("Scrap", 3),
            AbsentUtils.SStack(WhenDeployedSplitIntoBamAndBoozle.Name)
        ];
        card.charmSlots *= 2;
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "United forever";
}