namespace AbsentAvalanche.Scriptables.TargetConstraints;

public class TargetConstraintCurrentCounter : TargetConstraint
{
    public int targetCounter = 1;
    
    public override bool Check(Entity target)
    {
        return target.counter.current == targetCounter != not;
    }

    public override bool Check(CardData targetData)
    {
        return targetData.counter == targetCounter != not;
    }
}