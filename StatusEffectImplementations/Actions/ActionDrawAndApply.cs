using System.Collections;

namespace AbsentAvalanche.StatusEffectImplementations.Actions;

public class ActionDrawAndApply(
    Character character,
    Entity applier,
    int count = 1,
    CardData.StatusEffectStacks[] stacks = null,
    float pauseBetween = 0.1f)
    : PlayAction
{
    private int _count = count;

    public override IEnumerator Run()
    {
        if (_count <= 0 || !character.drawContainer || !character.handContainer || !character.discardContainer)
            yield break;

        Events.InvokeCardDraw(_count);
        while (_count > 0)
        {
            yield return Sequences.Wait(pauseBetween);
            var top = character.drawContainer.GetTop();
            if (!top)
            {
                Events.InvokeCardDrawEnd();
                yield return Sequences.ShuffleTo(character.discardContainer, character.drawContainer);
                top = character.drawContainer.GetTop();
                Events.InvokeCardDraw(_count);
            }

            if (top)
            {
                yield return Sequences.CardMove(top, [character.handContainer]);
                character.handContainer.TweenChildPositions();
                foreach (var stack in stacks)
                    ActionQueue.Stack(new ActionApplyStatusQuicklyPlease(top, applier, stack.data, stack.count));
            }

            _count--;
        }

        Events.InvokeCardDrawEnd();
        ActionQueue.Stack(new ActionRevealAll(character.handContainer));
    }
}