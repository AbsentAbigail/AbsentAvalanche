#region

using AbsentAvalanche.Helpers;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(CardFramesSystem), nameof(CardFramesSystem.TrySetFrameLevel), typeof(CardData), typeof(int))]
public class CardFramesSystemPatches
{
    [UsedImplicitly]
    private static void Postfix(ref bool __result, CardFramesSystem __instance, CardData cardData, int level)
    {
        if (__result || !cardData.cardType.miniboss)
        {
            return;
        }

        if (!cardData.name.EndsWith("Leader"))
        {
            return;
        }
        var nonLeaderCard = GetNonLeader(cardData.name);

        if (!nonLeaderCard)
        {
            return;
        }

        if (nonLeaderCard.cardType.miniboss || __instance.frameLevels.TryGetValue(nonLeaderCard.name, out var num) && num >= level)
        {
            return;
        }
        CardDiscoverSystem.instance.DiscoverCard(nonLeaderCard);
        __instance.frameLevels[nonLeaderCard.name] = level;
        __instance.newFrameLevels[nonLeaderCard.name] = level;
        __result = true;
        LogHelper.Log($"Gilded non-leader version of Leader: {cardData.title} [{cardData.name}]");
    }

    private static CardData GetNonLeader(string name)
    {
        var newName = name.Remove(name.Length - "Leader".Length);
        LogHelper.Log("try get name: " + newName);
        var cardData = Absent.Instance.Get<CardData>(newName);
        LogHelper.Log("new card: " + cardData?.name);
        return cardData;
    }
}