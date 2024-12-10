using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

public class BubblesAndCuddles() : AbstractCompanion(Name, "Bubbles And Cuddles", 24, null, 6,
    pools: Pools.None,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(WhenDeployedSplitIntoBubblesAndCuddles.Name)
        ];
        card.charmSlots *= 2;
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "Bubbly feelings of cuddly love";
}