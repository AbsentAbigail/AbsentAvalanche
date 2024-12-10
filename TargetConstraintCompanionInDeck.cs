using System.Linq;
using AbsentUtilities;

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
        LogHelper.Log("Count " + companions);
        LogHelper.Log("Not " + not);
        LogHelper.Log("Check " + (companions > 0 == !not));
        return companions > 0 == !not;
    }
}