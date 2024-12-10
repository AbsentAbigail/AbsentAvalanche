using System;
using System.Collections;
using System.Linq;
using AbsentUtilities;

namespace AbsentAvalanche.StatusEffects.Implementations;

public class StatusEffectInstantAddRandomCharm : StatusEffectInstant
{
    public bool addToTarget;
    public CardUpgradeData[] customList;
    public Predicate<CardUpgradeData> Predicate;

    public override IEnumerator Process()
    {
        var i = GetAmount();
        while (i-- > 0)
        {
            var charm = GetCharm().Clone();
            AddUpgrade(charm);
        }

        Campaign.PromptSave();
        yield return base.Process();
    }

    private CardUpgradeData GetCharm()
    {
        if (customList is { Length: > 0 })
            return customList.RandomItem();

        var predicate = AbsentUtils.GetStatusOf<StatusEffectInstantAddRandomCharm>(name).Predicate;

        var component = References.Player.GetComponent<CharacterRewards>();
        var result = component.Pull<CardUpgradeData>(target, "Charms", 1, false,
            c => c is CardUpgradeData charm
                 && (predicate is null || predicate.Invoke(charm))
                 && (!addToTarget || charm.CanAssign(target))
        );
        return result.Length > 0
            ? result[0]
            : AbsentUtils.GetCardUpgrade(
                "CardUpgradeAcorn"); // Quitting and reentering breaks CharacterRewards, so add a default charm
    }

    private void AddUpgrade(CardUpgradeData charm)
    {
        var inventory = References.PlayerData.inventory;
        if (!addToTarget)
        {
            inventory.upgrades.Add(charm);
            LogHelper.Log($"Added {charm.name} to inventory");
            return;
        }

        var deckCopy = inventory.deck.FirstOrDefault(c => c.id == target.data.id);
        if (deckCopy == null)
            return;
        charm.Assign(deckCopy);
        LogHelper.Log($"Assigned [{charm.name}] to [{target.name}] in deck");
    }
}