#region

using System.Collections;
using System.Linq;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectInstantApplyRandom : StatusEffectInstantApplyEffect
{
    public StatusEffectData[] possibleEffects;

    public override IEnumerator Process()
    {
        effectToApply = possibleEffects
            .Where(e => e.targetConstraints.All(tc => tc.Check(target)))
            .ToArray().RandomItem();
        effectToApply ??= possibleEffects[0];
        return base.Process();
    }
}