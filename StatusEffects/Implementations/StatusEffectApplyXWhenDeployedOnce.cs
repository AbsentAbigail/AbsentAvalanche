using System.Collections;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectApplyXWhenDeployedOnce : StatusEffectApplyXWhenDeployed
{
    public override void Init()
    {
        OnEnable += Activate;
        OnCardMove += Activate;
    }

    private new IEnumerator Activate(Entity entity)
    {
        if (contextEqualAmount)
        {
            var amount = contextEqualAmount.Get(entity);
            yield return Run(GetTargets(hackyHit), amount);
        }
        else
        {
            yield return Run(GetTargets(hackyHit));
        }

        yield return Remove();
    }
}