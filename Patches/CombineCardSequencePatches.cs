using System;
using System.Collections;
using System.Linq;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(CombineCardSequence), "Run", typeof(CardData[]), typeof(CardData))]
public class CombineCardSequencePatches
{
    private static bool Prefix(ref IEnumerator __result, CombineCardSequence __instance, CardData[] cards,
        CardData finalCard)
    {
        var leader = cards.FirstOrDefault(c => c.cardType.miniboss);
        if (leader != default(CardData))
        {
            LeaderHelper.GiveUpgrade().Run(finalCard);
            finalCard.cardType = leader.cardType;
            finalCard.SetCustomData("OverrideCardType", leader.cardType.name);
        }
        
        var inventory = References.PlayerData.inventory;
        cards.Do(card =>
        {
            if (inventory.deck.All(c => card.id != c.id))
                return;
            
            card.upgrades.Do(upgrade =>
            {
                if (upgrade.type == CardUpgradeData.Type.Crown && card.cardType.miniboss)
                    return;
                References.PlayerData.inventory.upgrades.Add(upgrade);
            });
            
            foreach (var upgrade in card.upgrades.ToArray().Reverse())
                upgrade.UnAssign(card);
            card.upgrades.Clear();
            
            AdjustStats(card, finalCard);
            
            References.PlayerData.inventory.deck.Remove(card);
        });
        return true;
    }

    private static IEnumerator Postfix(IEnumerator __result, CombineCardSequence __instance, CardData[] cards, CardData finalCard)
    {
        yield return __result;
        
        if (!finalCard.cardType.miniboss)
            yield break;
        
        var deck = References.PlayerData.inventory.deck;
        if (deck.Remove(finalCard))
            deck.Insert(0, finalCard);            
    }

    private static void AdjustStats(CardData comboCard, CardData finalCard)
    {
        var originalCard = AbsentUtils.GetCard(comboCard.name);
        
        var healthDiff = comboCard.hp - originalCard.hp;
        var damageDiff = comboCard.damage - originalCard.damage;
        var counterDiff = comboCard.counter - originalCard.counter;

        if (finalCard.hasHealth)
            finalCard.hp = Math.Max(1, finalCard.hp + healthDiff);
        if (finalCard.hasAttack)
            finalCard.damage += damageDiff;
        if (finalCard.counter > 0)
            finalCard.counter = Math.Max(1, finalCard.counter + counterDiff);
    }
}