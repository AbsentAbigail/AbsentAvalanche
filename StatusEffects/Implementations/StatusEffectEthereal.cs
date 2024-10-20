using System.Collections;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectEthereal : StatusEffectData
{
    public override void Init()
    {
        OnTurnEnd += Check;
    }

    public override bool RunTurnEndEvent(Entity entity)
    {
        if (!target.enabled)
            return false;
        
        if (entity != target.owner.entity)
            return false;
        
        return target.InHand();
    }

    private IEnumerator Check(Entity entity)
    {
        yield return Sequences.Wait(0.2f);
        const int amount = 1;
        yield return CountDown(target, amount);
    }
    
    public override IEnumerator RemoveStacks(int amount, bool removeTemporary)
    {
        count -= amount;
        if (removeTemporary)
            temporary -= amount;
        if (count <= 0)
        {
            yield return Remove();
            yield return target.Kill();
        }
        target.PromptUpdate();
    }
}