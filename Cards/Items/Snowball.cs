using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Items;

public class Snowball() : AbstractItem(Name, "Snowball",
    0, true, Pools.None, playOnHand: false, subscribe: card =>
    {
        card.attackEffects =
        [
            AbsentUtils.SStack("Snow", 2)
        ];
        card.startWithEffects = [AbsentUtils.SStack(OnCardPlayedBoostSelf.Name)];
        card.traits = [AbsentUtils.TStack("Zoomlin")];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}