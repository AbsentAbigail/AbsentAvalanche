#region

using System.Collections.Generic;
using System.Linq;
using AbsentAvalanche.Builders.Traits;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(CardControllerBattle), "Press")]
public class CardControllerBattlePatchesPress
{ 
    public static string GrandmasterName = Absent.GetTrait(Grandmaster.Name).name;
    [UsedImplicitly]
    private static void Postfix(CardControllerBattle __instance) //__instance is the instance calling the method
    {
        var owner = __instance.owner;
        var pressEntity = __instance.pressEntity;
        if (pressEntity == null)
        {
            return;
        }
        if (pressEntity.data.cardType.name == "Boss")
        {
            return;
        }
        var moveEnemies = Battle.GetCards(owner)
            .Any(entity => !entity.silenced && entity.traits.Any(tstack => tstack.data.name == GrandmasterName));

        if (pressEntity.owner == owner || !moveEnemies)
        {
          return;
        }
        if (!__instance.TryDrag(pressEntity))
        {
          return;
        }
        
        __instance.UnHover(pressEntity);
        NavigationState.Start(new NavigationStateCard(pressEntity));
    }
}

[HarmonyPatch(typeof(CardControllerBattle), "Release")]
public class CardControllerBattlePatchesRelease
{    
    [UsedImplicitly]
    private static bool Prefix(CardControllerBattle __instance) //__instance is the instance calling the method
    {
    if (!(bool) __instance.dragging)
      return false;
    var retainPosition = false;
    var retainRotation = false;
    var retainScale = false;
    var retainDrawOrder = false;
    if (__instance.enabled)
    {
      if (InputSwitcher.justSwitched)
        __instance.dragging.TweenToContainer();
      else if ((bool) __instance.hoverContainer && __instance.hoverContainer.canBePlacedOn && __instance.hoverContainer == __instance.owner.discardContainer && __instance.dragging.owner == __instance.owner)
      {
        if (__instance.dragging.CanRecall())
        {
          var action = new ActionMove(__instance.dragging, __instance.hoverContainer);
          if (Events.CheckAction(action))
          {
            Events.InvokeDiscard(__instance.dragging);
            if (Battle.IsOnBoard(__instance.dragging))
              __instance.owner.freeAction = true;
            ActionQueue.Add(action);
            ActionQueue.Add(new ActionEndTurn(__instance.owner));
            __instance.enabled = false;
            retainDrawOrder = true;
          }
        }
        __instance.hoverContainer.UnHover();
      }
      else
      {
        switch (__instance.dragging.data.playType)
        {
          case Card.PlayType.Play:
            if (!__instance.dragging.NeedsTarget)
            {
              if (!(bool) __instance.hoverContainer || !__instance.dragging.InContainer(__instance.hoverContainer))
              {
                var action = new ActionTrigger(__instance.dragging, __instance.owner.entity);
                if (Events.CheckAction(action))
                {
                  ActionQueue.Add(action);
                  ActionQueue.Add(new ActionReduceUses(__instance.dragging));
                  ActionQueue.Add(new ActionResetOffset(__instance.dragging));
                  ActionQueue.Add(new ActionEndTurn(__instance.owner));
                  __instance.enabled = false;
                  retainPosition = true;
                  retainRotation = true;
                  retainDrawOrder = true;
                }
              }
              break;
            }
            if (__instance.dragging.data.playOnSlot)
            {
              var cardContainer = __instance.dragging.targetMode.TargetRow ? __instance.hoverContainer : __instance.hoverSlot;
              if (__instance.dragging.CanPlayOn(cardContainer))
              {
                var action = new ActionTriggerAgainst(__instance.dragging, __instance.owner.entity, null, cardContainer);
                if (Events.CheckAction(action))
                {
                  if (ShoveSystem.Active)
                    ShoveSystem.Fix = true;
                  ActionQueue.Add(action);
                  ActionQueue.Add(new ActionReduceUses(__instance.dragging));
                  ActionQueue.Add(new ActionResetOffset(__instance.dragging));
                  ActionQueue.Add(new ActionEndTurn(__instance.owner));
                  __instance.enabled = false;
                  retainPosition = true;
                  retainRotation = true;
                  retainDrawOrder = true;
                }
              }
              break;
            }
            if (__instance.dragging.targetMode.TargetRow)
            {
              if (__instance.dragging.CanPlayOn(__instance.hoverContainer))
              {
                var action = new ActionTriggerAgainst(__instance.dragging, __instance.owner.entity, null, __instance.hoverContainer);
                if (Events.CheckAction(action))
                {
                  ActionQueue.Add(action);
                  ActionQueue.Add(new ActionReduceUses(__instance.dragging));
                  ActionQueue.Add(new ActionResetOffset(__instance.dragging));
                  ActionQueue.Add(new ActionEndTurn(__instance.owner));
                  __instance.enabled = false;
                  retainPosition = true;
                  retainRotation = true;
                  retainDrawOrder = true;
                }
              }
              break;
            }
            if ((bool) __instance.hoverEntity && __instance.hoverEntity != __instance.dragging)
            {
              var action = new ActionTriggerAgainst(__instance.dragging, __instance.owner.entity, __instance.hoverEntity, null);
              if (Events.CheckAction(action))
              {
                ActionQueue.Add(action);
                ActionQueue.Add(new ActionReduceUses(__instance.dragging));
                ActionQueue.Add(new ActionResetOffset(__instance.dragging));
                ActionQueue.Add(new ActionEndTurn(__instance.owner));
                __instance.enabled = false;
                retainPosition = true;
                retainRotation = true;
                retainDrawOrder = true;
              }
            }
            break;
          case Card.PlayType.Place:
            if ((bool) __instance.hoverSlot && !__instance.dragging.actualContainers.Contains(__instance.hoverSlot) && __instance.hoverSlot.canBePlacedOn && __instance.hoverSlot.owner == __instance.dragging.owner)
            {
              if (__instance.hoverSlot.Count < __instance.hoverSlot.max)
              {
                var action = new ActionMove(__instance.dragging, __instance.hoverSlot);
                if (Events.CheckAction(action))
                {
                  var flag = Battle.IsOnBoard(__instance.dragging) && Battle.IsOnBoard(__instance.hoverSlot.Group);
                  Events.InvokeEntityPlace(__instance.dragging, [
                    __instance.hoverSlot
                  ], (flag ? 1 : 0) != 0);
                  ActionQueue.Add(action);
                  ActionQueue.Add(new ActionEndTurn(__instance.owner));
                  if (flag)
                    __instance.owner.freeAction = true;
                  __instance.enabled = false;
                }
                break;
              }
              Dictionary<Entity, List<CardSlot>> shoveData;
              if (ShoveSystem.CanShove(__instance.hoverSlot.GetTop(), __instance.dragging, out shoveData))
              {
                var action = new ActionMove(__instance.dragging, __instance.hoverSlot);
                if (Events.CheckAction(action))
                {
                  var flag = Battle.IsOnBoard(__instance.dragging) && Battle.IsOnBoard(__instance.hoverSlot.Group);
                  ShoveSystem.Fix = true;
                  Events.InvokeEntityPlace(__instance.dragging, [
                    __instance.hoverSlot
                  ], (flag ? 1 : 0) != 0);
                  ActionQueue.Add(new ActionShove(shoveData));
                  ActionQueue.Add(action);
                  ActionQueue.Add(new ActionEndTurn(__instance.owner));
                  if (flag)
                    __instance.owner.freeAction = true;
                  __instance.enabled = false;
                }
              }
            }
            break;
          case Card.PlayType.None:
            break;
        }
      }
      if (ActionQueue.Empty)
        __instance.dragging.TweenToContainer();
    }
    __instance.TweenUnHover(__instance.dragging, retainScale, retainPosition, retainRotation, retainDrawOrder);
    __instance.DragEnd();
    __instance.UnHover();
    return false;
    }
}