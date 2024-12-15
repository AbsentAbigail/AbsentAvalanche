using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Companion;

public class SherbaAndCuddles() : AbstractCompanion(Name, "Snuggle Buddies", 26, 0, 6,
    pools: Pools.None,
    subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(WhenDeployedSplitIntoSherbaAndCuddles.Name),
            AbsentUtils.SStack(WhenDeployedApplySnowToEnemies.Name),
        ];
        card.charmSlots *= 2;
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "No place is better than the arms of your best friend";
}