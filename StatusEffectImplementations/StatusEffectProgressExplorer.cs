#region

using System.Collections;
using System.Linq;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectProgressExplorer : StatusEffectInstant
{
    public string countDownName;
    public bool countDownAll;

    public override IEnumerator Process()
    {
        var explorerStatus = target.statusEffects.FirstOrDefault(status => status.name == countDownName);

        if (explorerStatus != null)
        {
            var amount = countDownAll ? explorerStatus.count : count;

            yield return explorerStatus.CountDown(target, amount);
            var deckCopy = GetDeckCopy();
            RemoveFromDeckCopy(deckCopy, amount);
            target.display.promptUpdateDescription = true;
            target.PromptUpdate();
        }
        
        yield return Remove();
    }

    private CardData GetDeckCopy()
    {
        var deckCopy = References.PlayerData.inventory.deck.FirstOrDefault(card => card.id == target.data.id);
        return deckCopy;
    }

    private void RemoveFromDeckCopy(CardData card, int amount)
    {
        if (card is null)
        {
            LogHelper.Log("No deck copy found");
            return;
        }
        var effect = card.startWithEffects.FirstOrDefault(status => status.data.name == countDownName);

        effect?.count -= amount;
    }
}