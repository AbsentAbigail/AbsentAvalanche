#region

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AbsentAvalanche.Builders.Traits;
using Dead;
using UnityEngine;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations.Actions;

internal class ActionEquip(Entity equipment, Entity target) : PlayAction
{
    private static StatusEffectData[] _illegalStatusEffects = [];
    private static TraitData[] _illegalTraits = [
        Absent.GetTrait("Zoomlin"),
        Absent.GetTrait("Noomlin"),
        Absent.GetTrait("Consume"),
        Absent.GetTrait(Combo.Name)
    ];
    private static CardContainer ReserveContainer => References.Player.reserveContainer;
    
    public override IEnumerator Run()
    {
        yield return CheckFreeAction();
        
        MarkCustomData();
        
        target.hp = Add(target.hp, equipment.hp);
        target.damage = Add(target.damage, equipment.damage);

        yield return GainEffects();

        yield return MoveToReserveSequence();
    }
    
    private IEnumerator MoveToReserveSequence()
    {
        const float animationTime = 0.4f;
        equipment.RemoveFromContainers();
        ReserveContainer.Add(equipment);
        LeanTween.scale(equipment.gameObject, Vector2.zero, animationTime).setEaseInBack();
        yield return Sequences.Wait(animationTime);
        ReserveContainer.SetChildPosition(equipment);
        foreach (var container in equipment.preContainers)
        {
            container.TweenChildPositions();
        }
    }

    private IEnumerator GainEffects()
    {
        target.attackEffects = CardData.StatusEffectStacks.Stack(target.attackEffects, equipment.attackEffects).ToList();
        
        var list = equipment.statusEffects.Where(e => e is not StatusEffectNextPhase || _illegalStatusEffects.Contains(e)).ToList();
        
        foreach (var trait in equipment.traits.ToArray())
        {
            foreach (var passiveEffect in trait.passiveEffects)
            {
                list.Remove(passiveEffect);
            }

            if (_illegalTraits.Contains(trait.data))
            {
                continue;
            }

            var num = trait.count - trait.tempCount;
            if (num > 0)
            {
                target.GainTrait(trait.data, num);
            }
        }

        foreach (var item in list)
        {
            yield return StatusEffectSystem.Apply(target, equipment, item, item.count);
        }

        yield return target.UpdateTraits();
        target.display.promptUpdateDescription = true;
        target.PromptUpdate();
    }

    private void MarkCustomData()
    {
        var id = equipment.data.id;
        equipment.data.customData ??= new Dictionary<string, object>();
        equipment.data.customData["absent.equipment"] = id;
        
        ulong[] array = [];
        if (target.data.customData?.TryGetValue("absent.equipments", out var equipments) ?? false)
        {
            array = ((SaveCollection<ulong>)equipments).collection;
        }

        array = [
            ..array,
            id
        ];
        
        target.data.customData ??= new Dictionary<string, object>();
        target.data.customData["absent.equipments"] = new SaveCollection<ulong>(array);
        
        var traits = new SaveCollection<(string, int)>(equipment.traits
            .Where(t => !_illegalTraits.Contains(t.data))
            .Select<Entity.TraitStacks, (string, int)>(a => (a.data.name, a.count)).ToArray());
        target.data.customData["absent.equipped.traits"] = traits;
    }

    private IEnumerator CheckFreeAction()
    {
        var effectFreeAction = (StatusEffectFreeAction)equipment.statusEffects.FirstOrDefault(data => data is StatusEffectFreeAction);

        if (effectFreeAction is null)
        {
            yield break;
        }
        
        effectFreeAction.hasEffect = false;
        if (effectFreeAction.target.display is Card display)
        {
            display.itemHolderPet?.Used();
            Events.InvokeNoomlinUsed(effectFreeAction.target);
            var mover = display.gameObject.AddComponent<Mover>();
            mover.velocity = new Vector3(PettyRandom.Range(0.0f, 1f).WithRandomSign(), -12f, 0.0f);
            mover.frictMult = 0.8f;
            effectFreeAction.target.wobbler?.WobbleRandom();
            yield return Sequences.Wait(0.6f);
        }
        effectFreeAction.target.owner.freeAction = true;
    }
    
    private static Stat Add(Stat stat1, Stat stat2)
    {
        return new Stat(
            stat1.current + stat2.current,
            stat1.max + stat2.max
        );
    }
}