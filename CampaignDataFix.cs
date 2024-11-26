using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbsentUtilities;
using UnityEngine;

namespace AbsentAvalanche;

public static class CampaignDataFix
{
    public static Task SaveCampaignData()
    {
        LogHelper.Warn("~CAMPAIGN GENERATED~");
        var preset = Campaign.instance.preset.text;
        SaveSystem.SaveCampaignData(Campaign.Data.GameMode, "absent.preset", preset);

        var rewards = References.Player.GetComponent<CharacterRewards>().poolLookup;
        var rewards2 = new Dictionary<string, SaveCollection<string>>();
        foreach (var category in rewards.Keys) rewards2[category] = rewards[category].list.ToSaveCollectionOfNames();

        SaveSystem.SaveCampaignData(Campaign.Data.GameMode, "absent.rewards", rewards2);
        return Task.CompletedTask;
    }

    public static void LoadCampaignData()
    {
        if (!Campaign.instance || Campaign.Data == null)
            return;
        LogHelper.Warn("~CAMPAIGN LOADED~");
        var preset = SaveSystem.LoadCampaignData<string>(Campaign.Data.GameMode, "absent.preset", null);
        Campaign.instance.preset ??= new TextAsset(preset);
        LogHelper.Warn("Reusing campaign preset from custom data:\n" + preset);

        var rewards2 =
            SaveSystem.LoadCampaignData<Dictionary<string, SaveCollection<string>>>(Campaign.Data.GameMode,
                "absent.rewards", []);
        var rewards = new Dictionary<string, CharacterRewards.Pool>();
        foreach (var category in rewards2.Keys)
        {
            rewards[category] = new CharacterRewards.Pool { list = [] };
            foreach (var name in rewards2[category].collection)
            {
                var type = category switch
                {
                    "Items" or "Units" => typeof(CardData),
                    "Charms" => typeof(CardUpgradeData),
                    "Modifiers" => typeof(GameModifierData),
                    _ => null
                };
                if (type == null)
                {
                    LogHelper.Error(
                        $"Unrecognised CharacterReward category [{category}]. Couldn't assign decide which AddressableLoader group it belonged to");
                    continue;
                }

                var data = AddressableLoader.Get<DataFile>(type.Name, name);
                if (data)
                    rewards[category].Add(data);
            }
        }

        var poolLookup = References.Player.GetComponent<CharacterRewards>()?.poolLookup;
        if (poolLookup == null)
            return;

        foreach (var key in rewards.Keys)
            poolLookup[key] = rewards[key];
        LogHelper.Warn("Reusing character rewards from custom data:");
        foreach (var pool in poolLookup)
        {
            LogHelper.Warn($"{pool.Key}");
            LogHelper.Log(string.Join(", ", pool.Value.list.ToArrayOfNames().DefaultIfEmpty("")));
        }
    }
}