using System.Collections;
using System.Linq;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectInstantCountDownStatus : StatusEffectInstant
{
    public string[] types;
    public bool negative;
    public bool positive;

    public override IEnumerator Process()
    {
        var remove = target.statusEffects.Where(status =>
            (types.Length == 0 || types.Contains(status.type)) &&
            (positive != status.IsNegativeStatusEffect() ||
             negative == status.IsNegativeStatusEffect()));

        foreach (var status in remove.ToArray())
            yield return status.CountDown(target, 1);

        yield return Remove();
    }
}