#region

using System.Collections;
using System.Linq;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectInstantReplaceInDeck : StatusEffectInstant
{
    public CardData replaceWith;

    public override IEnumerator Process()
    {
        var inventory = References.PlayerData.inventory;
        var current = target.data;
        var transformInto = replaceWith.Clone();

        // Re-assign charms or drop to inventory
        foreach (var upgradeCopy in current.upgrades.Select(upgrade => Absent.GetCardUpgrade(upgrade.name).Clone()))
        {
            if (upgradeCopy.CanAssign(transformInto))
                upgradeCopy.Assign(transformInto);
            else
                inventory.upgrades.Add(upgradeCopy);
        }
        
        // Keep card type if leader
        if (current.cardType.name == "Leader")
        {
            transformInto.cardType = current.cardType;
            transformInto.SetCustomData("OverrideCardType", "Leader");
        }

        var card = CardManager.Get(transformInto, null, References.Player, false, true);
        
        //Checks for renames
        var baseCard = Absent.GetCard(current.name);
        if (baseCard.title != current.title)
        {
            transformInto.forceTitle = current.title;
            card.SetName(current.title);
            Events.InvokeRename(card.entity, current.title);
        }

        if (current.cardType.name == "Leader")
            inventory.deck.Insert(0, card.entity.data);
        else
            inventory.deck.Add(card.entity.data);
        inventory.deck.RemoveWhere(c => c.id == current.id);
        
        CardDiscoverSystem.instance.DiscoverCard(transformInto);
        yield return base.Process();
    }
}