using System;
using System.Collections.Generic;
using System.Linq;
using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;
using JetBrains.Annotations;

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(CardSaveData), "Load", typeof(bool))]
public class CardSaveDataPatches
{
    [UsedImplicitly]
    private static void Postfix(ref CardData __result, CardSaveData __instance) //__instance is the instance calling the method
    {
        var saveData = __result.customData;
        if (saveData == null || !saveData.ContainsKey("Sarcophagus")) return;
        try
        {
            var customData = saveData.Get<SaveCollection<object>>("Sarcophagus");

            var cardData = AbsentUtils.GetCard((string)customData[0]).InstantiateKeepName();

            ((SaveCollection<string>)customData[4]).collection
                .Do(a => AbsentUtils.GetCardUpgrade(a).Assign(cardData));

            cardData.traits =
            [
                .. ((SaveCollection<(string, int)>)customData[3]).collection
                .ToDictionary(a => a.Item1, a => a.Item2)
                .Select(a => AbsentUtils.TStack(a.Key, a.Value))
            ];

            cardData.attackEffects =
            [
                .. ((SaveCollection<(string, int)>)customData[1]).collection
                    .ToDictionary(a => a.Item1, a => a.Item2)
                    .Select(a => AbsentUtils.SStack(a.Key, a.Value))
            ];

            cardData.startWithEffects =
            [
                .. ((SaveCollection<(string, int)>)customData[2]).collection
                    .ToDictionary(a => a.Item1, a => a.Item2)
                    .Select(a => AbsentUtils.SStack(a.Key, a.Value))
            ];

            var summon = AbsentUtils.GetStatusOf<StatusEffectSummon>(SummonSarcophagus.Name).InstantiateKeepName();
            summon.summonCard = cardData;

            var instantSummon = AbsentUtils.GetStatusOf<StatusEffectInstantSummon>(InstantSummonSarcophagus.Name)
                .InstantiateKeepName();
            instantSummon.targetSummon = summon;

            var applyX = AbsentUtils.GetStatusOf<StatusEffectApplyX>(WhenDestroyedSummonSarcophagus.Name)
                .InstantiateKeepName();
            applyX.effectToApply = instantSummon;
            applyX.textInsert = $"<{cardData.title}>";
            
            for (var i = 0; i < __result.startWithEffects.Length; i++)
            {
                if (__result.startWithEffects[i].data.name != $"{AbsentUtils.GetModInfo().Mod.GUID}.{WhenDestroyedSummonSarcophagus.Name}")
                    continue;
                __result.startWithEffects[i] = new CardData.StatusEffectStacks(applyX, __result.startWithEffects[i].count);
                LogHelper.Warn("Replaced effect");
            }
        }
        catch (Exception e)
        {
            LogHelper.Error("Loading Sarcophagus data failed");
            LogHelper.Error(e.Message);
            LogHelper.Error(e.StackTrace);
        }
    }
}