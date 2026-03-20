#region

using System;
using System.Linq;
using AbsentAvalanche.Builders.StatusEffects;
using HarmonyLib;
using JetBrains.Annotations;

#endregion

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(CardSaveData), "Load", typeof(bool))]
public class CardSaveDataPatches
{
    public static string SarcophagusName = Absent.GetStatus(WhenDestroyedSummonSarcophagus.Name).name;
    
    [UsedImplicitly]
    private static void Postfix(ref CardData __result, CardSaveData __instance) //__instance is the instance calling the method
    {
        var saveData = __result.customData;
        if (saveData == null || !saveData.ContainsKey("Sarcophagus")) return;
        try
        {
            var customData = saveData.Get<SaveCollection<object>>("Sarcophagus");

            var cardData = Absent.GetCard((string)customData[0]).InstantiateKeepName();

            ((SaveCollection<string>)customData[4]).collection
                .Do(a => Absent.GetCardUpgrade(a).Assign(cardData));

            cardData.traits =
            [
                .. ((SaveCollection<(string, int)>)customData[3]).collection
                .ToDictionary(a => a.Item1, a => a.Item2)
                .Select(a => Absent.TStack(a.Key, a.Value))
            ];

            cardData.attackEffects =
            [
                .. ((SaveCollection<(string, int)>)customData[1]).collection
                    .ToDictionary(a => a.Item1, a => a.Item2)
                    .Select(a => Absent.SStack(a.Key, a.Value))
            ];

            cardData.startWithEffects =
            [
                .. ((SaveCollection<(string, int)>)customData[2]).collection
                    .ToDictionary(a => a.Item1, a => a.Item2)
                    .Select(a => Absent.SStack(a.Key, a.Value))
            ];

            var summon = Absent.GetStatusOf<StatusEffectSummon>(SummonSarcophagus.Name).InstantiateKeepName();
            summon.summonCard = cardData;

            var instantSummon = Absent.GetStatusOf<StatusEffectInstantSummon>(InstantSummonSarcophagus.Name)
                .InstantiateKeepName();
            instantSummon.targetSummon = summon;

            var applyX = Absent.GetStatusOf<StatusEffectApplyX>(WhenDestroyedSummonSarcophagus.Name)
                .InstantiateKeepName();
            applyX.effectToApply = instantSummon;
            applyX.textInsert = $"<{cardData.title}>";
            
            for (var i = 0; i < __result.startWithEffects.Length; i++)
            {
                if (__result.startWithEffects[i].data.name != SarcophagusName)
                    continue;
                __result.startWithEffects[i] = new CardData.StatusEffectStacks(applyX, __result.startWithEffects[i].count);
                Logger.Warn("Replaced effect");
            }
        }
        catch (Exception e)
        {
            Logger.Error("Loading Sarcophagus data failed");
            Logger.Error(e.Message);
            Logger.Error(e.StackTrace);
        }
    }
}