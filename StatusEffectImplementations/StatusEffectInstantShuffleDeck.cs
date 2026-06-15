using System.Collections;
using UnityEngine;

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectInstantShuffleDeck : StatusEffectInstant
{
    public override IEnumerator Process()
    {
        yield return ShuffleTo(References.Player.discardContainer, References.Player.drawContainer);
        yield return base.Process();
    }

    private static IEnumerator ShuffleTo(
        CardContainer fromContainer,
        CardContainer toContainer,
        float delayBetween = 0.05f)
    {
        if (!toContainer || !fromContainer)
        {
            yield break;
        }

        while (!fromContainer.Empty)
        {
            yield return Sequences.CardMove(fromContainer[Random.Range(0, fromContainer.Count)], [
                toContainer
            ]);
            if (delayBetween > 0.0)
            {
                yield return Sequences.Wait(delayBetween);
            }
        }

        if (delayBetween <= 0.0)
        {
            toContainer.SetChildPositions();
        }
    }
}