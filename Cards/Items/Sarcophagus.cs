using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Items;

public class Sarcophagus() : AbstractItem(Name, "Sarcophagus",
    pools: Pools.None, subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(Ethereal.Name, 3),
            AbsentUtils.SStack(WhenDestroyedSummonSarcophagus.Name, 2)
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    protected override string IdleAnimation => "PulseAnimationProfile";
}