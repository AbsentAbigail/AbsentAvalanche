#region

using AbsentAvalanche.Builders.Flavours;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffectImplementations;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

#endregion

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(Card), nameof(Card.SetDescription))]
public class CardPatches
{
    public static string[][] Flavours = [
        ["verdego.wildfrost.specialdelivery.AbigailIsakai", Bubbles.Name]
    ];

    private static readonly string SarcophagusEffectName = Absent.PrefixGuid(WhenDestroyedSummonSarcophagus.Name);

    [UsedImplicitly]
    private static void Postfix(Card __instance)
    {
        FlavourBoxes(__instance);
        SarcophagusInsert(__instance);
        UsesDisplay(__instance);
        EquipmentDisplay(__instance);
    }

    private static void FlavourBoxes(Card card)
    {
        foreach (var flavour in Flavours)
        {
            if (flavour[0] != card.name)
            {
                continue;
            }
            
            card.keywords.Add(Absent.GetKeyword(flavour[1]));
            break;
        }
    }
    
    private static void SarcophagusInsert(Card card)
    {
        var cardData = card.entity.data;
        cardData.startWithEffects.Do(stack =>
        {
            if (stack.data.name != SarcophagusEffectName)
                return;
            var stackData = (StatusEffectApplyX)stack.data;
            var insertCard = ((StatusEffectInstantSummon)stackData.effectToApply)
                .targetSummon.summonCard;
            card.mentionedCards.Add(insertCard);
            stackData.textInsert = $"<{insertCard.title}>";
        });
    }

    private static void UsesDisplay(Card card)
    {
        var uses = card.entity.uses;
        if (uses.max > 1)
        {
            card.descText.text += $"\n<color=#{Color.gray.ToHexRGB()}>Uses: {uses.current}/{uses.max}";
        }
    }
    
    private static void EquipmentDisplay(Card card)
    {
        if (!References.Player?.reserveContainer)
        {
            return;
        }

        if (References.Player.reserveContainer.entities.Contains(card.entity))
        {
            return;
        }

        if (card.entity.FindStatus<StatusEffectEquip>("equip") == null)
        {
            return;
        }

        foreach (var equipment in References.Player.reserveContainer)
        {
            var equipEffect = equipment.FindStatus<StatusEffectEquip>("equip");
            if (equipEffect is null)
            {
                return;
            }
            if (equipEffect.cardId == card.entity.data.id)
            {
                card.mentionedCards.Add(equipment.data);
            }
        }
    }
}