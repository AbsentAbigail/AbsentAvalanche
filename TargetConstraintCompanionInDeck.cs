using System.Linq;

namespace AbsentAvalanche;

public class TargetConstraintCompanionInDeck : TargetConstraint
{
    public override bool Check(Entity target)
    {
        return Check(target.data);
    }

    public override bool Check(CardData targetData)
    {
        var companions = References.PlayerData.inventory.deck.Count(c => c.cardType.name == "Friendly");
        return companions > 0 == !not;
    }
}