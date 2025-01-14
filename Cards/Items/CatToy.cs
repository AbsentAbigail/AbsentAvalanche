using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Cards.Items;

public class CatToy() : AbstractItem(Name, "Cat Toy",
    0, true, Pools.None, playOnHand: false, subscribe: card =>
    {
        card.startWithEffects = [AbsentUtils.SStack(Cat.Name, 2)];
    })
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
    public override string FlavourText => "here pspsps";
}