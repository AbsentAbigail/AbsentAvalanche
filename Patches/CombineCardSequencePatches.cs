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
            card.upgrades.Clear();
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
}