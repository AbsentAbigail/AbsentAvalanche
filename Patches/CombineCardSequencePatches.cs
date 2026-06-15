using System;
using System.Collections;
using System.Linq;
using AbsentAvalanche.Builders.Cards.PilotLeaders;
using AbsentAvalanche.Helpers;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(CombineCardSequence), nameof(CombineCardSequence.Run), typeof(CardData[]), typeof(CardData))]
public class CombineCardSequencePatches
{
    private static readonly string[] AddCardData =
    [
        Absent.PrefixGuid(AmeliaDraw.Name),
        Absent.PrefixGuid(AmeliaFreePlay.Name),
        Absent.PrefixGuid(AmeliaFrenzy.Name),
        Absent.PrefixGuid(AmeliaTrigger.Name),
    ];
    
    [UsedImplicitly]
    private static bool Prefix(ref IEnumerator __result, CombineCardSequence __instance, CardData[] cards,
        CardData finalCard)
    {
        var leader = cards.FirstOrDefault(c => c.cardType.miniboss);
        if (leader != null)
        {
            LeaderHelper.GiveUpgrade().Run(finalCard);
            finalCard.cardType = leader.cardType;
            finalCard.SetCustomData("OverrideCardType", leader.cardType.name);
        }
        
        var inventory = References.PlayerData.inventory;
        cards.Do(card =>
        {
            if (inventory.deck.All(c => card.id != c.id))
            {
                return;
            }
            
            card.upgrades.Do(upgrade =>
            {
                if (upgrade.type == CardUpgradeData.Type.Crown && card.cardType.miniboss)
                {
                    return;
                }
                References.PlayerData.inventory.upgrades.Add(upgrade);
            });

            foreach (var upgrade in card.upgrades.ToArray().Reverse())
            {
                upgrade.UnAssign(card);
            }
            card.upgrades.Clear();

            if (AddCardData.Contains(card.name))
            {
                AddStatsAndEffects(card, finalCard);
            }
            else
            {
                AdjustStats(card, finalCard);
            }
            
            References.PlayerData.inventory.deck.Remove(card);
        });
        return true;
    }

    [UsedImplicitly] // Insert Leader at front of deck
    private static IEnumerator Postfix(IEnumerator result, CombineCardSequence __instance, CardData[] cards, CardData finalCard)
    {
        yield return result;

        if (!finalCard.cardType.miniboss)
        {
            yield break;
        }
        
        var deck = References.PlayerData.inventory.deck;
        if (deck.Remove(finalCard))
        {
            deck.Insert(0, finalCard);
        }            
    }

    private static void AdjustStats(CardData comboCard, CardData finalCard)
    {
        var originalCard = Absent.GetCard(comboCard.name);
        
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
    
    private static void AddStatsAndEffects(CardData card, CardData finalCard)
    {
        finalCard.hp += card.hp;
        finalCard.damage += card.damage;
        finalCard.counter = Math.Min(card.counter, finalCard.counter);
        finalCard.attackEffects = CardData.StatusEffectStacks.Stack(finalCard.attackEffects, card.attackEffects);
        finalCard.startWithEffects = CardData.StatusEffectStacks.Stack(finalCard.startWithEffects, card.startWithEffects);
        CardData.TraitStacks.Stack(ref finalCard.traits, card.traits);
    }
}