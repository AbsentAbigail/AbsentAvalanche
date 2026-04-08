#region

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

namespace AbsentAvalanche.Scriptables.Scripts;

public class ScriptRunScriptsOnCardsInDeck : Script
{
    public CardScript[] scripts;
    public TargetConstraint[] constraints;
    public Vector2Int countRange;
    public bool all = true;
    public bool includeReserve;

    public override IEnumerator Run()
    {
        List<CardData> cardDataList = [];
        AddRangeIfConstraints(cardDataList, References.PlayerData.inventory.deck, constraints);
        if (includeReserve)
            AddRangeIfConstraints(cardDataList, References.PlayerData.inventory.reserve, constraints);
        if (cardDataList.Count <= 0)
        {
            yield break;
        }
        Affect(cardDataList);
    }

    private static void AddRangeIfConstraints(
        ICollection<CardData> collection,
        CardDataList toAdd,
        TargetConstraint[] constraints)
    {
        foreach (var cardData in toAdd)
            AddIfConstraints(collection, cardData, constraints);
    }

    private static void AddIfConstraints(
        ICollection<CardData> collection,
        CardData item,
        TargetConstraint[] constraints)
    {
        if (constraints.Any(tc => !tc.Check(item)))
            return;
        collection.Add(item);
    }

    public void Affect(IReadOnlyCollection<CardData> cards)
    {
        var num = countRange.Random();
        Debug.Log($"[{name}] Affecting [{string.Join(", ", cards)}]");
        foreach (var target in cards.InRandomOrder())
        {
            foreach (var cardScript in scripts)
            {
                cardScript.Run(target);
            }

            --num;
            if (!all && num <= 0)
            {
                break;
            }
        }
    }
}