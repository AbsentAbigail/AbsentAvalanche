using System.Collections.Generic;
using System.Linq;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects.TargetModes;

// Code nabbed and edited from Pokefrosts Pluck
internal class TargetModeHighestAttack : TargetMode
{
    public override Entity[] GetPotentialTargets(
        Entity entity,
        Entity target,
        CardContainer targetContainer)
    {
        var hashSet = new HashSet<Entity>();
        if ((bool)targetContainer)
        {
            if (targetContainer.Count > 0) hashSet.Add(GetTarget(targetContainer));
        }
        else if ((bool)target)
        {
            if (target.containers.Length == 0)
                return hashSet.Count <= 0 ? null : hashSet.ToArray();
            var cardContainer = target.containers.RandomItem();
            if (cardContainer.Count > 0)
                hashSet.Add(GetTarget(cardContainer));
        }
        else
        {
            var rowIndices = Battle.instance.GetRowIndices(entity);
            foreach (var rowIndex in rowIndices) AddTargets(entity, hashSet, rowIndex);

            if (hashSet.Count != 0)
                return hashSet.Count <= 0 ? null : hashSet.ToArray();
            var rowCount = Battle.instance.rowCount;
            for (var j = 0; j < rowCount; j++)
                if (!rowIndices.Contains(j))
                    AddTargets(entity, hashSet, j);
        }

        return hashSet.Count <= 0 ? null : hashSet.ToArray();
    }

    // Target mode for items
    public override bool CanTarget(Entity entity)
    {
        var flag = true;
        var containers = entity.containers;
        var entityDamage = GetDamage(entity);
        foreach (var cardContainer in containers)
            for (var num = 0; num < cardContainer.Count; num++)
            {
                var test = cardContainer[num];
                var testDamage = GetDamage(test);
                if (!test || !test.enabled || !test.alive || !test.canBeHit)
                    continue;
                if (entityDamage < testDamage)
                {
                    flag = false;
                    break;
                }
                if (entityDamage == testDamage && num < cardContainer.IndexOf(entity))
                {
                    flag = false;
                    break;
                }
            }

        return flag;
    }

    public override CardSlot[] GetTargetSlots(CardSlotLane row)
    {
        return [row.slots[row.max - 1]];
    }

    private void AddTargets(Entity entity, HashSet<Entity> targets, int rowIndex)
    {
        var enemiesInRow = entity.GetEnemiesInRow(rowIndex);
        var target =  GetTarget(enemiesInRow);
        if ((bool)target)
        {
            targets.Add(target);
            return;
        }

        target = GetEnemyCharacter(entity);
        if ((bool)target) targets.Add(target);
    }

    // Search for highest attack
    private static Entity GetTarget(IList<Entity> targets)
    {
        var highest = 0;
        Entity target = null;
        foreach (var entity in targets)
        {
            if (!(bool)entity || !entity.enabled || !entity.alive || !entity.canBeHit)
                continue;
            var damage = GetDamage(entity);
            if (target is not null && highest >= damage)
                continue;
            highest = damage;
            target = entity;
        }
    
        return target;
    }
    
    private static int GetDamage(Entity entity) => entity.damage.current + entity.tempDamage.Value;
}