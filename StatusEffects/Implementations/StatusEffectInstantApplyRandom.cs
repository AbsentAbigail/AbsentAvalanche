using System.Collections;
using System.Linq;

namespace AbsentAvalanche.StatusEffects.Implementations;

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