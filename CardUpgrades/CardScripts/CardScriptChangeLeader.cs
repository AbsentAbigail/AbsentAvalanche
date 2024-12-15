using System.Collections;
using System.Linq;
using AbsentUtilities;
using UnityEngine;

namespace AbsentAvalanche.CardUpgrades.CardScripts;

public class CardScriptChangeLeader : CardScript
{
    public override void Run(CardData target)
    {
        var inventory = References.PlayerData.inventory;

        var promotion = inventory.deck.FirstOrDefault(c => c.cardType.name == "Friendly");
        if (promotion == default(CardData))
            return;

        LogHelper.Warn($"[CardScriptChangeLeader] Demoting [{target.name}]");
        LogHelper.Warn($"[CardScriptChangeLeader] Promoting [{promotion.name}]");

        var targetClone = target.Clone(false);
        targetClone.upgrades.RemoveAllWhere(upgrade => upgrade.type == CardUpgradeData.Type.Crown);
        targetClone.cardType = AbsentUtils.GetCardType("Friendly");
        targetClone.SetCustomData("OverrideCardType", "Friendly");
        inventory.deck.Add(targetClone);
        
        CardScriptDestroyCard.RemoveFromDeck(target);
        CardScriptDestroyCard.DestroyEntities(target);

        var promotionClone = promotion.Clone(false);
        promotionClone.cardType = AbsentUtils.GetCardType("Leader");
        promotionClone.SetCustomData("OverrideCardType", "Leader");
        promotionClone.upgrades.RemoveAllWhere(upgrade => upgrade.type == CardUpgradeData.Type.Crown);
        LeaderHelper.GiveUpgrade().Run(promotionClone);
        inventory.deck.Insert(0, promotionClone);

        CardScriptDestroyCard.RemoveFromDeck(promotion);
        CardScriptDestroyCard.DestroyEntities(promotion);
        
        var targetCard = CardManager.Get(targetClone, FindObjectOfType<CardControllerDeck>(), 
            References.Player, false, true);
        var promotionCard = CardManager.Get(promotionClone, FindObjectOfType<CardControllerDeck>(), 
            References.Player, false, true);
        References.instance.StartCoroutine(Animation(targetCard));
        References.instance.StartCoroutine(Animation(promotionCard, true));
    }

    // Copied from Pokefrosts CardScriptCopy
    private static IEnumerator Animation(Card card, bool leader = false)
    {
        yield return new WaitForSeconds(1f);

        yield return card.UpdateData();

        var deckDisplaySequence = FindObjectOfType<DeckDisplaySequence>();
        if (deckDisplaySequence is null)
            yield break;

        var entity = card.entity;
        if (leader)
            deckDisplaySequence.activeCardsGroup.GetGrid(card).Insert(0, entity);
        else
            deckDisplaySequence.activeCardsGroup.GetGrid(card).Add(entity);
        deckDisplaySequence.UpdatePositions();
    }
}