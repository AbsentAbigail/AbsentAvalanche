using System.Collections;

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectInstantRecall : StatusEffectInstant
{
    public bool forceRecall;
    
    public override IEnumerator Process()
    {
        if (forceRecall || target.CanRecall())
        {
            var action = new ActionMove(target, References.Player.discardContainer);
            Events.InvokeDiscard(target);
            ActionQueue.Add(action);
        }
        else
        {
            yield return NoTargetTextSystem.Run(target, NoTargetType.CantMove);
        }
        
        yield return Remove();
    }
}