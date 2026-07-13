using AbsentAvalanche.EventHooks;
using UnityEngine.Events;

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectMailpuppy : StatusEffectApplyX
{
    public static event UnityAction OnDelivery;

    public static void InvokeDelivery()
    {
        OnDelivery?.Invoke();
    }

    public override void Init()
    {
        OnDelivery += ReduceCounter;
    }

    private void OnDestroy()
    {
        OnDelivery -= ReduceCounter;
    }

    private void ReduceCounter()
    {
        ActionQueue.Add(new ActionSequence(Run([target])));
        MailpuppyHooks.AddInvitationToDeck();
    }
}