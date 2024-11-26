using AbsentAvalanche.StatusEffects;
using AbsentUtilities;

namespace AbsentAvalanche.Cards.Items;

public class CatToy() : AbstractItem(Name, "Cat Toy",
    0, true, Pools.None, playOnHand: false, subscribe: card =>
    {
        card.startWithEffects = [AbsentUtils.SStack(Cat.Name, 2)];
    })
{
    public const string Name = "CatToy";
    public override string FlavourText => "here pspsps";
}