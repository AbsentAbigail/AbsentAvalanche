using System.Collections;

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectInstantSwapHealthAttack : StatusEffectInstant
{
    public override IEnumerator Process()
    {
        (target.hp.current, target.hp.max, target.damage.current, target.damage.max) = (target.damage.current, target.damage.max, target.hp.current, target.hp.max);
        yield return Remove();
    }
}