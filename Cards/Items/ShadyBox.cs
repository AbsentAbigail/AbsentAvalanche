using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Items;

public class ShadyBox() : AbstractItem(Name, "Forgotten Box",
    pools: Pools.None, subscribe: card =>
    {
        card.startWithEffects =
        [
            AbsentUtils.SStack(Ethereal.Name, 4),
            AbsentUtils.SStack("When Destroyed Apply Frenzy To RandomAlly")
        ];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "A cardboard box under the bed... What could be inside?";
}