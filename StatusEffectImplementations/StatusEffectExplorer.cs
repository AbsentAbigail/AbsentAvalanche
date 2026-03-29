#region

using System.Collections;
using System.Linq;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectExplorer : StatusEffectData
{
    public string[][] evolutions;

    public override IEnumerator RemoveStacks(int amount, bool removeTemporary)
    {
        count -= amount;
        if (removeTemporary)
            temporary -= amount;
        if (count <= 0)
        {
            yield return ReplaceInDeck();
            yield return Remove();
            foreach (var targetStatusEffect in target.statusEffects.OfType<StatusEffectExplorer>().Clone())
            {
                yield return targetStatusEffect.Remove();
            }
        }
        target.PromptUpdate();
    }

    private CardData GetReplacement()
    {
        var evolutionArrays = Absent.GetStatusOf<StatusEffectExplorer>(name).evolutions;
        return (from pair in evolutionArrays where Absent.PrefixGuid(pair[0]).Equals(target.name) select Absent.GetCard(pair[1])).FirstOrDefault()?.Clone();
    }

    private IEnumerator ReplaceInDeck()
    {
        var inventory = References.PlayerData.inventory;
        var currentCardData = target.data;
        if (inventory.deck.All(card => card.id != currentCardData.id))
        {
            yield break;
        }
        
        var transformInto = GetReplacement();
        transformInto.SetId(CardData.idCurrent);

        if (transformInto == null)
        {
            yield break;
        }
        
        // Re-assign charms or drop to inventory
        foreach (var upgradeCopy in currentCardData.upgrades.Select(upgrade => Absent.GetCardUpgrade(upgrade.name).Clone()))
        {
            if (upgradeCopy.CanAssign(transformInto) || upgradeCopy.type == CardUpgradeData.Type.Crown)
            {
                upgradeCopy.Assign(transformInto);
            }
            else
            {
                inventory.upgrades.Add(upgradeCopy);
            }
        }
        
        // Keep card type if leader
        if (currentCardData.cardType.name == "Leader")
        {
            transformInto.cardType = currentCardData.cardType;
            transformInto.SetCustomData("OverrideCardType", "Leader");
        }

        var card = CardManager.Get(transformInto, null, References.Player, false, true);
        
        //Checks for renames
        var baseCard = Absent.GetCard(currentCardData.name);
        if (baseCard.title != currentCardData.title)
        {
            transformInto.forceTitle = currentCardData.title;
            card.SetName(currentCardData.title);
            Events.InvokeRename(card.entity, currentCardData.title);
        }

        if (currentCardData.cardType.name == "Leader")
        {
            inventory.deck.Insert(0, card.entity.data);
        }
        else
        {
            inventory.deck.Add(card.entity.data);
        }
        inventory.deck.RemoveWhere(c => c.id == currentCardData.id);
        CardDiscoverSystem.instance.DiscoverCard(transformInto);
    }
}