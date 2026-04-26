namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectApplyXInstantScriptableApplier : StatusEffectApplyXInstant
{
    public override int GetAmount(Entity entity, bool equalAmount = false, int equalTo = 0)
    {
        if (scriptableAmount != null)
            return scriptableAmount.Get(applier);
        return !equalAmount ? GetAmount() : equalTo;
    }
}