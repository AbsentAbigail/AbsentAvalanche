using System.Linq;
using AbsentAvalanche.Builders.Cards.Clunkers;
using AbsentAvalanche.Builders.Cards.Companions;
using AbsentAvalanche.Builders.Cards.Items;
using AbsentAvalanche.Helpers;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(CardFramesSystem), nameof(CardFramesSystem.TrySetFrameLevel), typeof(CardData), typeof(int))]
public class CardFramesSystemPatches
{
    private static readonly (string, string)[] AssociatedCards =
    [
        (LeafPip.Name, Nest.Name),
        (CuddlePip.Name, Nest.Name),
        (April.Name, WoolGrenade.Name),
        (April.Name, GoolWrenade.Name),
        (SalvoKitty.Name, Missile.Name),
        (RazorPlush.Name, HogCosplay.Name),
        (PillowFortress.Name, Pillow.Name),
        (MamaWombat.Name, WombatPoop.Name),
        
        (BubblesAndCuddles.Name, Bubbles.Name),
        (BubblesAndCuddles.Name, Cuddles.Name),
        (Bamboozle.Name, Bam.Name),
        (Bamboozle.Name, Boozle.Name),
        (Catci.Name, Catcitten.Name),
        (SherbaAndCuddles.Name, Sherba.Name),
        (SherbaAndCuddles.Name, Cuddles.Name),
        (AliceAndNami.Name, Alice.Name),
        (AliceAndNami.Name, Nami.Name),
        (AprilAndMay.Name, April.Name),
        (AprilAndMay.Name, May.Name),
        (BubblesAndKiki.Name, Bubbles.Name),
        (BubblesAndKiki.Name, Kiki.Name),
        (EmeraldAndSally.Name, Emerald.Name),
        (EmeraldAndSally.Name, Sally.Name),
        
        (BubblesAndCuddles.Name + "Leader", Bubbles.Name),
        (BubblesAndCuddles.Name + "Leader", Cuddles.Name),
        (Bamboozle.Name + "Leader", Bam.Name),
        (Bamboozle.Name + "Leader", Boozle.Name),
        (SherbaAndCuddles.Name + "Leader", Sherba.Name),
        (SherbaAndCuddles.Name + "Leader", Cuddles.Name),
        (AliceAndNami.Name + "Leader", Alice.Name),
        (AliceAndNami.Name + "Leader", Nami.Name),
        (AprilAndMay.Name + "Leader", April.Name),
        (AprilAndMay.Name + "Leader", May.Name),
        (BubblesAndKiki.Name + "Leader", Bubbles.Name),
        (BubblesAndKiki.Name + "Leader", Kiki.Name),
        (EmeraldAndSally.Name + "Leader", Emerald.Name),
        (EmeraldAndSally.Name + "Leader", Sally.Name),
    ];
    
    [UsedImplicitly]
    private static void Postfix(ref bool __result, CardFramesSystem __instance, CardData cardData, int level)
    {
        GildLeader(ref __result, __instance, cardData, level);
        GildAssociated(__instance, cardData, level);
    }

    private static void GildAssociated(CardFramesSystem cardFramesSystem, CardData cardData, int level)
    {
        var associatedCardNames = AssociatedCards.Where(a => Absent.PrefixGuid(a.Item1) == cardData.name).Select(a => a.Item2);
        foreach (var associatedCardName in associatedCardNames)
        {
            var associatedCard = Absent.GetCard(associatedCardName);
            LogHelper.Log($"Gilded {cardData.title}, also gilding {associatedCard.title}");
            if (cardFramesSystem.frameLevels.TryGetValue(associatedCard.name, out var num) && num >= level)
            {
                continue;
            }
            CardDiscoverSystem.instance.DiscoverCard(associatedCard);
            cardFramesSystem.frameLevels[associatedCard.name] = level;
            cardFramesSystem.newFrameLevels[associatedCard.name] = level;
        }
    }

    private static void GildLeader(ref bool result, CardFramesSystem cardFramesSystem, CardData cardData, int level)
    {
        if (result || !cardData.cardType.miniboss)
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

        if (nonLeaderCard.cardType.miniboss || cardFramesSystem.frameLevels.TryGetValue(nonLeaderCard.name, out var num) && num >= level)
        {
            return;
        }
        CardDiscoverSystem.instance.DiscoverCard(nonLeaderCard);
        cardFramesSystem.frameLevels[nonLeaderCard.name] = level;
        cardFramesSystem.newFrameLevels[nonLeaderCard.name] = level;
        result = true;
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