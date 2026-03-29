#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AbsentAvalanche.Helpers;
using HarmonyLib;
using JetBrains.Annotations;
using Random = System.Random;

#endregion

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(SelectLeader), nameof(SelectLeader.CreateLeader), typeof(ClassData))]
public class SelectLeaderCreateLeaderPatches
{
    private static readonly Predicate<CardUpgradeData> Predicate = charm =>
        charm.scripts is null || charm.scripts.Count(s => s.GetType().Name != "CardScriptAddStatsWhenCharmed") == 0;

    private static readonly Dictionary<ClassData,  CardUpgradeData[]> CharmPool = [];
    
    [UsedImplicitly]
    private static Card Postfix(Card __result, SelectLeader __instance, ClassData classData)
    {
        var amount = SaveSystem.statsSaver.LoadValue("leader_charms", Absent.PrefixGuid("data"), 0);
        if (amount == 0)
        {
            return __result;
        }
        var seed = SaveSystem.statsSaver.LoadValue("source", Absent.PrefixGuid("data"), 0);
        var random = GetRandomFromCard(__result, seed);
        __result.entity.data.charmSlots += amount;
        var array = GetCharmPool(classData);
        for (var i = 0; i < amount; i++)
        {
            var filteredArray = array.Where(upgrade => upgrade.CanAssign(__result.entity.data))
                .OrderBy(upgrade => upgrade.name).ToArray();
            var randomCharm = filteredArray[random.Next(filteredArray.Length)].name;
            LeaderHelper.GiveUpgrade(randomCharm).Run(__result.entity.data);
        }

        return __result;
    }

    private static CardUpgradeData[] GetCharmPool(ClassData classData)
    {
        if (CharmPool.TryGetValue(classData, out var pool))
        {
            return pool;
        }
        
        var charmPools = classData.rewardPools.Where(rewardPool => rewardPool.type == nameof(RewardPool.Type.Charms)).Select(rewardPool => rewardPool.list);
        CharmPool[classData] = charmPools.Aggregate((a, b) => [..a, ..b])
            .Where(dataFile => dataFile is CardUpgradeData charm && Predicate.Invoke(charm)).Cast<CardUpgradeData>()
            .ToArray();
        return CharmPool[classData];
    }

    private static Random GetRandomFromCard(Card card, int seed)
    {
        var md5 = MD5.Create();
        var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(card.entity.data.name));
        var i = BitConverter.ToInt32(hash, 0);
        return new Random(i + seed);
    }
}