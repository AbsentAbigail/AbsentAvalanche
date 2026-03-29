#region

using System.Collections;
using System.Linq;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectInstantAddStatusPermanent : StatusEffectInstantApplyEffect
{
    public override IEnumerator Process()
    {
        var card = GetDeckCopy();
        if (card is not null)
        {
            var amount = scriptableAmount ? scriptableAmount.Get(target) : GetAmount();
            AddStatusToCardData(card, amount);
        }
        yield return base.Process();
    }

    private CardData GetDeckCopy()
    {
        var deckCopy = References.PlayerData.inventory.deck.FirstOrDefault(card => card.id == target.data.id);
        return deckCopy;
    }
    
    private void AddStatusToCardData(CardData cardData, int amount)
    {
        var existingStatus = cardData.startWithEffects.FirstOrDefault(statusStacks => statusStacks.data.name == effectToApply.name);
        if (existingStatus != null)
        {
            existingStatus.count += amount;
        }
        else
        {
            cardData.startWithEffects =
            [
                .. cardData.startWithEffects,
                new CardData.StatusEffectStacks(
                    effectToApply,
                    amount)
            ];
        }
    }
}