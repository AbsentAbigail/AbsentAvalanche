#region

using System.Collections;
using System.Linq;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffectImplementations.Actions;
using HarmonyLib;
using UnityEngine;
using Extensions = Deadpan.Enums.Engine.Components.Modding.Extensions;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectEquip : StatusEffectData
{
    private static CardContainer ReserveContainer => References.Player.reserveContainer;
    private static CardContainer HandContainer => References.Player.handContainer;
    public ulong? cardId;
    
    public override void Init()
    {
        Events.OnActionQueued += ActionQueued;
        Events.OnEntityKilled += EntityKilled;
    }
    
    public void OnDestroy()
    {
        Events.OnActionQueued -= ActionQueued;
        Events.OnEntityKilled -= EntityKilled;

        target?.data?.customData?.Remove("absent.equipped.traits");
    }

    public override object GetMidBattleData()
    {
        return cardId;
    }

    public override void RestoreMidBattleData(object data)
    {
        cardId = (ulong)data;
        var card = Battle.GetAllCards().FirstOrDefault(card => card.data.id == cardId);
        if (card == null)
        {
            return;
        }
        card.display.promptUpdateDescription = true;
        card.PromptUpdate();
    }


    private void ActionQueued(PlayAction action)
    {
        if (!IsItem(target))
        {
            return;
        }
        
        ReplaceTrigger(action);
        RemoveReduceUses(action);
    }

    private void EntityKilled(Entity entity, DeathType deathType)
    {
        if (!ReserveContainer.Contains(target))
        {
            return;
        }

        if (entity.data.id != cardId)
        {
            return;
        }
        ActionQueue.Stack(new ActionSequence(ReturnToHandSequence(entity.transform.position)), fixedPosition: true);
        ActionQueue.Stack(new ActionRunEnableEvent(target), fixedPosition: true);
    }

    private IEnumerator ReturnToHandSequence(Vector3 fromPosition)
    {
        const float animationTime = 0.4f;
        target.transform.position = fromPosition;
        target.transform.localScale = Vector2.zero;
        LeanTween.scale(target.gameObject, HandContainer.GetChildScale(target), animationTime).setEaseOutBack();
        target.flipper.FlipUpInstant();
        target.wobbler.WobbleRandom(2f);
        target.curveAnimator.Ping();
        yield return Sequences.Wait(animationTime + 0.2f);
        yield return Sequences.CardMove(target, [HandContainer]);
    }
    
    private static bool IsItem(Entity entity)
    {
        return entity?.data.cardType.item ?? false;
    }
    
    private void ReplaceTrigger(PlayAction action)
    {
        if (action is not ActionTriggerAgainst trigger || trigger.entity != target)
        {
            return;
        }

        cardId = trigger.target.data.id;
        ActionQueue.Insert(ActionQueue.IndexOf(action), new ActionEquip(trigger.entity, trigger.target));
        ActionQueue.Remove(action);
    }

    private void RemoveReduceUses(PlayAction action)
    {
        if (action is not ActionReduceUses reduceUses || reduceUses.entity != target)
        {
            return;
        }
        ActionQueue.Remove(action);
    }

    public override bool RunBeginEvent()
    {
        if (!IsItem(target))
        {
            return false;
        }

        target.statusEffects.Where(s => s is StatusEffectChangeTargetMode).Do(s =>
        {
            var changeTargetMode = (StatusEffectChangeTargetMode)s;
            changeTargetMode.pre = Extensions.GetTargetMode("TargetModeBasic");
            changeTargetMode.targetMode = changeTargetMode.pre;
            changeTargetMode.RunEndEvent();
        });
        return false;
    }
}