using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectSummonWithCrown : StatusEffectData
{
  public CardData summonCard;
  public StatusEffectData gainTrait;
  public CardType setCardType;
  public CardUpgradeData upgrade = Absent.GetCardUpgrade("Crown");
  
  public AssetReference effectPrefabRef;
  public bool unsubRequired;
  public CardSlot[] toSummon;

  public override void Init()
  {
    if (target.data.playOnSlot)
    {
      Events.OnActionPerform += ActionPerform;
      unsubRequired = true;
    }
    OnCardPlayed += CardPlayed;
  }

  public void OnDestroy()
  {
    if (!unsubRequired)
    {
      return;
    }
    Events.OnActionPerform -= ActionPerform;
  }

  public void ActionPerform(PlayAction action)
  {
    if (target.silenced || action is not ActionTriggerAgainst actionTriggerAgainst ||
        !(bool)(Object)actionTriggerAgainst.targetContainer || !(actionTriggerAgainst.entity == target))
    {
      return;
    }
    toSummon = actionTriggerAgainst.targetContainer switch
    {
      CardSlot cardSlot => [cardSlot],
      CardSlotLane row => target.targetMode.GetTargetSlots(row),
      _ => toSummon
    };
  }

  public override bool RunCardPlayedEvent(Entity entity, Entity[] targets)
  {
    return entity == target && !target.silenced;
  }

  public IEnumerator CardPlayed(Entity entity, Entity[] targets)
  {
    if (toSummon == null)
    {
      var list = new HashSet<CardContainer>();
      list.AddRange(entity.actualContainers);
      if (list.Count > 0 && list.ToArray().RandomItem() is CardSlot cardSlot)
        toSummon =
        [
          cardSlot
        ];
    }
    if (toSummon != null && toSummon.Length != 0)
    {
      var controller = target.display.hover.controller;
      target.curveAnimator.Ping();
      var cardSlotArray = toSummon;
      foreach (var container in cardSlotArray)
      {
        yield return TrySummon(container, controller, target);
      }
    }
    toSummon = null;
    yield return null;
  }

  public IEnumerator Summon(
    CardContainer container,
    CardController controller,
    Entity summonedBy,
    StatusEffectData[] withEffects = null,
    int withEffectsAmount = 0,
    UnityAction<Entity> onComplete = null)
  {
    if (!(bool)(Object)container)
    {
      yield break;
    }

    Entity entity = null;
    yield return CreateCard(summonCard, container, controller, e => entity = e);
    if (withEffectsAmount > 0 && withEffects != null)
    {
      foreach (var effectData in withEffects)
      {
        yield return StatusEffectSystem.Apply(entity, null, effectData, withEffectsAmount);
      }
    }

    if ((bool)(Object)gainTrait)
    {
      ActionQueue.Stack(new ActionSequence(Animate(entity, new CardData.StatusEffectStacks(gainTrait, 1))), true);
    }
    else
    {
      ActionQueue.Stack(new ActionSequence(Animate(entity)), true);
    }
    var action = new ActionSequence(ShoveIfNecessary(entity, container))
    {
      note = "Shove If Necessary"
    };
    ActionQueue.Stack(action, true);
    ActionQueue.Stack(new ActionRunEnableEvent(entity), true);
    ActionQueue.Stack(new ActionMove(entity, container), true);
    Events.InvokeEntitySummoned(entity, summonedBy);

    onComplete?.Invoke(entity);
  }

  public IEnumerator SummonCopy(
    Entity toCopy,
    CardContainer container,
    CardController controller,
    Entity summonedBy,
    StatusEffectData[] withEffects = null,
    int withEffectsAmount = 0,
    UnityAction<Entity> onComplete = null)
  {
    Entity entity = null;
    yield return CreateCard(toCopy.data, container, controller, e =>
    {
      entity = e;
      e.startingEffectsApplied = true;
    });
    yield return CopyStatsAndEffects(entity, toCopy);
    yield return SummonPreMade(entity, container, controller, summonedBy, withEffects, withEffectsAmount, onComplete);
  }

  public IEnumerator SummonPreMade(
    Entity preMade,
    CardContainer container,
    CardController controller,
    Entity summonedBy,
    StatusEffectData[] withEffects = null,
    int withEffectsAmount = 0,
    UnityAction<Entity> onComplete = null)
  {
    if (withEffectsAmount > 0 && withEffects != null)
    {
      foreach (var t in withEffects)
      {
        yield return StatusEffectSystem.Apply(preMade, null, t, withEffectsAmount);
      }
    }

    if ((bool)(Object)gainTrait)
    {
      yield return Animate(preMade, new CardData.StatusEffectStacks(gainTrait, 1));
    }
    else
    {
      yield return Animate(preMade);
    }
    Events.InvokeEntitySummoned(preMade, summonedBy);
    onComplete?.Invoke(preMade);
    yield return ShoveIfNecessary(preMade, container);
    yield return new ActionRunEnableEvent(preMade).Run();
    yield return new ActionMove(preMade, container).Run();
  }

  public static IEnumerator ShoveIfNecessary(Entity entity, CardContainer container)
  {
    if (container is CardSlot { Empty: false } cardSlot &&
        ShoveSystem.CanShove(cardSlot.GetTop(), entity, out var shoveData) && shoveData != null)
    {
      yield return ShoveSystem.DoShove(shoveData, true);
    }
  }

  public IEnumerator CopyStatsAndEffects(Entity entity, Entity toCopy)
  {
    toCopy.data.TryGetCustomData("splitOriginalId", out var num, toCopy.data.id);
    entity.data.SetCustomData("splitOriginalId", num);
    var list = toCopy.statusEffects.Where(e =>
    {
      if (e.count <= e.temporary || e.IsNegativeStatusEffect())
      {
        return false;
      }
      return e.HasDescOrIsKeyword || e.isStatus;
    }).ToList();
    foreach (var trait in toCopy.traits)
    {
      foreach (var passiveEffect in trait.passiveEffects)
      {
        list.Remove(passiveEffect);
      }
      var amount = trait.count - trait.tempCount;
      if (amount > 0)
      {
        entity.GainTrait(trait.data, amount);
      }
    }

    foreach (var effectData in list)
    {
      yield return StatusEffectSystem.Apply(entity, effectData.applier, effectData, effectData.count - effectData.temporary);
    }
    entity.attackEffects = CardData.StatusEffectStacks.Stack(entity.attackEffects, toCopy.attackEffects).Select(a => a.Clone()).ToList();
    entity.damage = toCopy.damage;
    entity.hp = toCopy.hp;
    entity.counter = toCopy.counter;
    entity.counter.current = entity.counter.max;
    entity.uses = toCopy.uses;
    entity.display.promptUpdateDescription = true;
    entity.PromptUpdate();
    yield return entity.UpdateTraits();
  }

  public IEnumerator Animate(Entity entity, params CardData.StatusEffectStacks[] withEffects)
  {
    var handle = effectPrefabRef.InstantiateAsync(entity.transform);
    yield return handle;
    var component = handle.Result.GetComponent<CreateCardAnimation>();
    if (component != null)
    {
      yield return component.Run(entity, withEffects);
    }
  }

  public Card CreateCardCopy(CardData cardData, CardContainer container, CardController controller)
  {
    var data = cardData.Clone(false);
    if (setCardType)
    {
      data.cardType = setCardType;
    }
    if (data.upgrades.All(a => a.type != CardUpgradeData.Type.Crown))
    {
      upgrade.Clone().Assign(data);
    }
    var cardCopy = CardManager.Get(data, controller, container.owner, true, container.owner.team == References.Player.team);
    cardCopy.entity.flipper.FlipUpInstant();
    cardCopy.canvasGroup.alpha = 0.0f;
    container.Add(cardCopy.entity);
    var transform = cardCopy.transform;
    transform.localPosition = cardCopy.entity.GetContainerLocalPosition();
    transform.localEulerAngles = cardCopy.entity.GetContainerLocalRotation();
    transform.localScale = cardCopy.entity.GetContainerScale();
    container.Remove(cardCopy.entity);
    cardCopy.entity.owner.reserveContainer.Add(cardCopy.entity);
    return cardCopy;
  }

  public IEnumerator CreateCard(
    CardData cardData,
    CardContainer container,
    CardController controller,
    UnityAction<Entity> onComplete = null)
  {
    var cardCopy = CreateCardCopy(cardData, container, controller);
    onComplete?.Invoke(cardCopy.entity);
    yield return cardCopy.UpdateData();
  }

  public IEnumerator TrySummon(
    CardContainer container,
    CardController controller,
    Entity summonedBy)
  {
    if (container.Count < container.max)
    {
      yield return Summon(container, controller, summonedBy);
    }
    else
    {
      if (ShoveSystem.CanShove(container.GetTop(), target, out var shoveData))
      {
        yield return ShoveSystem.DoShove(shoveData, true);
        yield return Summon(container, controller, summonedBy);
      }
      else if (NoTargetTextSystem.Exists())
      {
        yield return NoTargetTextSystem.Run(target, NoTargetType.NoSpaceToSummon);
      }
    }
  }
}