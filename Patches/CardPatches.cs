using AbsentAvalanche.Cards.Clunkers;
using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(Card), "SetDescription")]
public class CardPatches
{
    public static string[][] Flavours = [];
    
    [UsedImplicitly]
    private static void Postfix(Card __instance) //__instance is the instance calling the method
    {
        // Flavour text boxes
        foreach (var flavour in Flavours)
            if (flavour[0] == __instance.name)
            {
                __instance.keywords.Add(AbsentUtils.GetKeyword(flavour[1]));
                break;
            }
        
        // Sarcophagus preview
        var statusName = AbsentUtils.GetStatus(WhenDestroyedSummonSarcophagus.Name).name;
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
    }
}