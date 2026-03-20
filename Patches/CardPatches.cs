#region

using AbsentAvalanche.Builders.Flavours;
using AbsentAvalanche.Builders.StatusEffects;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

#endregion

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(Card), "SetDescription")]
public class CardPatches
{
    public static string[][] Flavours = [
        ["verdego.wildfrost.specialdelivery.AbigailIsakai", Bubbles.Name]
    ];
    
    [UsedImplicitly]
    private static void Postfix(Card __instance) //__instance is the instance calling the method
    {
        // Flavour text boxes
        foreach (var flavour in Flavours)
            if (flavour[0] == __instance.name)
            {
                __instance.keywords.Add(Absent.GetKeyword(flavour[1]));
                break;
            }
        
        // Sarcophagus preview
        var statusName = Absent.GetStatus(WhenDestroyedSummonSarcophagus.Name).name;
        var card = __instance.entity.data;
        card.startWithEffects.Do(stack =>
        {
            if (stack.data.name != statusName)
                return;
            var s = (StatusEffectApplyX)stack.data;
            var c = ((StatusEffectInstantSummon)s.effectToApply)
                .targetSummon.summonCard;
            __instance.mentionedCards.Add(c);
            s.textInsert = $"<{c.title}>";
        });

        var uses = __instance.entity.uses;
        if (uses.max > 1)
        {
            __instance.descText.text += $"\n<color=#{Color.gray.ToHexRGB()}>Uses: {uses.current}/{uses.max}";
        }
    }
}