using System.Collections;
using System.Linq;
using AbsentAvalanche.StatusEffectImplementations.Actions;

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectInstantDrawAndApplyX : StatusEffectInstant
{
    public StatusEffectData[] effects;

    public override IEnumerator Process()
    {
        var player = References.Player;
        if (player.drawContainer.Empty && player.discardContainer.Empty)
        {
            if (NoTargetTextSystem.Exists())
                yield return NoTargetTextSystem.Run(target, NoTargetType.NoCardsToDraw);
        }
        else
        {
            ActionQueue.Stack(
                new ActionDrawAndApply(
                    player,
                    target,
                    GetAmount(),
                    effects.Select(s => new CardData.StatusEffectStacks(s, GetAmount())).ToArray()
                ), true
            );
        }

        yield return Remove();
    }

}