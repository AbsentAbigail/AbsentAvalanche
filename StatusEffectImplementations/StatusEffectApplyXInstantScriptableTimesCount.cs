namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectApplyXInstantScriptableTimesCount : StatusEffectApplyXInstant
{
    public override int GetAmount(Entity entity, bool equalAmount = false, int equalTo = 0)
    {
        return base.GetAmount(entity, equalAmount, equalTo) * GetAmount();
    }
}