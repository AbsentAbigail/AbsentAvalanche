#region

using System.Linq;

#endregion

namespace AbsentAvalanche.Scriptables.TargetConstraints;

public class TargetConstraintCompanionInDeck : TargetConstraint
{
    public override bool Check(Entity target)
    {
        return Check(target.data);
    }

    public override bool Check(CardData targetData)
    {
        var companions = References.PlayerData?.inventory?.deck?.Count(c => c.cardType.name == "Friendly") ?? 0;
        return companions > 0 == !not;
    }
}