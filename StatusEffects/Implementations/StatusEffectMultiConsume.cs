using System.Collections;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectMultiConsume : StatusEffectData
{
    private void OnDestroy()
    {
        Events.OnActionPerform -= CheckAction;
    }

    public override void Init()
    {
        Events.OnActionPerform += CheckAction;
    }

    public override IEnumerator RemoveStacks(int amount, bool removeTemporary)
    {
        count -= amount;
        if (removeTemporary)
            temporary -= amount;
        if (count <= 0)
        {
            yield return Remove();
            yield return new ActionConsume(target).Run();
        }

        target.PromptUpdate();
    }

    private void CheckAction(PlayAction action)
    {
        if (action is not ActionReduceUses actionReduceUses)
            return;
        
        if (actionReduceUses.entity != target)
            return;

        ActionQueue.Stack(
            new ActionSequence(RemoveStacks(1, false)) { note = "MultiConsume count down" }
        );
    }
}