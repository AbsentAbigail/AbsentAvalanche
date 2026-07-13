using System.Collections;
using AbsentAvalanche.Helpers;

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectInvitation : StatusEffectApplyX
{
    public override void Init()
    {
        OnCardPlayed += Check;
    }

    public override bool RunCardPlayedEvent(Entity entity, Entity[] targets)
    {
        return entity == target;
    }

    private IEnumerator Check(Entity entity, Entity[] targets)
    {
        var invitationTarget = target.data.GetCustomDataOrNull("absent.invitation") as string;
        if (invitationTarget.IsNullOrEmpty())
        {
            yield return PermaRemove();
            yield break;
        }
        
        foreach (var target1 in targets)
        {
            if (target1.data.name != invitationTarget)
            {
                continue;
            }
        
            yield return Run([target1]);
            StatusEffectMailpuppy.InvokeDelivery();
        }
        yield return PermaRemove();
    }

    private IEnumerator PermaRemove()
    {
        var deck = References.PlayerData.inventory.deck;
        deck.RemoveWhere(card => card.id == target.data.id);
        
        yield return target.Kill(DeathType.Consume);
    }
}