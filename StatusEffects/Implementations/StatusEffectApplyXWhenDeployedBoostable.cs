namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectApplyXWhenDeployedBoostable : StatusEffectApplyXWhenDeployed
{
    public override int GetAmount(Entity entity, bool equalAmount = false, int equalTo = 0)
    {
        var i = scriptableAmount ? GetAmount() : 1;
        return base.GetAmount(entity, equalAmount, equalTo) * i;
    }
}