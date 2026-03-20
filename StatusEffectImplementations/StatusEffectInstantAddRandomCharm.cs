#region

using System;
using System.Collections;
using System.Linq;

#endregion

namespace AbsentAvalanche.StatusEffectImplementations;

public class StatusEffectInstantAddRandomCharm : StatusEffectInstant
{
    public bool addToTarget;
    public CardUpgradeData[] customList;
    public Predicate<CardUpgradeData> predicate;

    public override IEnumerator Process()
    {
        var i = GetAmount();
        while (i-- > 0)
        {
            var charm = GetCharm().Clone();
            AddUpgrade(charm);

            CardDiscoverSystem.instance.DiscoverCharm(charm.name);
        }

        Campaign.PromptSave();
        yield return base.Process();
    }

    private CardUpgradeData GetCharm()
    {
        if (customList is { Length: > 0 })
            return customList.RandomItem();

        var predicate1 = Absent.GetStatusOf<StatusEffectInstantAddRandomCharm>(name).predicate;

        var component = References.Player.GetComponent<CharacterRewards>();
        var result = component.Pull<CardUpgradeData>(target, "Charms", 1, false,
            c => c is CardUpgradeData charm
                 && (predicate1 is null || predicate1.Invoke(charm))
                 && (!addToTarget || charm.CanAssign(target))
        );
        return result.Length > 0
            ? result[0]
            : Absent.GetCardUpgrade(
                "CardUpgradeAcorn"); // Give Acorn Charm if no matching charm was found
    }

    private void AddUpgrade(CardUpgradeData charm)
    {
        var inventory = References.PlayerData.inventory;
        if (!addToTarget)
        {
            inventory.upgrades.Add(charm);
            Logger.Log($"Added {charm.name} to inventory");
            return;
        }

        var deckCopy = inventory.deck.FirstOrDefault(card => card.id == target.data.id);
        if (deckCopy == null)
            return;
        charm.Assign(deckCopy);
        Logger.Log($"Assigned [{charm.name}] to [{target.name}] in deck");
    }
}