using System.Collections;
using System.Linq;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectInstantCountDownStatus : StatusEffectInstant
{
    public string[] types;
    public bool negative;
    public bool positive;
    public bool remove;

    public override IEnumerator Process()
    {
        var matchingStatus = target.statusEffects.Where(status =>
            (types.Length == 0 || types.Contains(status.type)) &&
            (positive != status.IsNegativeStatusEffect() ||
             negative == status.IsNegativeStatusEffect()));

        foreach (var status in matchingStatus.ToArray())
            yield return CountDown(status);

        yield return Remove();
    }

    private IEnumerator CountDown(StatusEffectData status)
    {
        if (remove)
            yield return status.Remove();
        else
            yield return status.CountDown(target, count);
    }
}