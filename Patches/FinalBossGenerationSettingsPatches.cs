using System.Collections.Generic;
using System.Linq;
using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(FinalBossGenerationSettings), "Process", typeof(CardData), typeof(IList<CardData>))]
public class FinalBossGenerationSettingsPatches
{
    [UsedImplicitly]
    private static void Prefix(FinalBossGenerationSettings __instance, CardData leader, IList<CardData> cards)
    {
        __instance.effectSwappers =
        [
            .. __instance.effectSwappers,
            CreateSwapper("SalvoKitty 1", OnCardPlayedAddMissileToHand.Name,
                OnCardPlayedAddCatomicBombToHand.Name,
                minBoost: -1, maxBoost: 0),
            CreateSwapper("SalvoKitty 2", GainCatWhenMissileIsPlayed.Name,
                OnCardPlayedGainCat.Name),
            CreateSwapper("BamAndBoozle", WhenDeployedSplitIntoBamAndBoozle.Name,
                WhenDeployedSplitIntoBamAndBoozleAscended.Name),
            CreateSwapper("Catci", WhenDeployedSplitIntoCatcusAndCatcitten.Name,
                WhenDeployedSplitIntoCatcusAndCatcittenAscended.Name)
        ];
    }

    private static FinalBossEffectSwapper CreateSwapper(string name, string effectToReplace, string replacement,
        int minBoost = 0, int maxBoost = 0)
    {
        return CreateSwapper(name, effectToReplace, [replacement], minBoost: minBoost, maxBoost: maxBoost);
    }

    private static FinalBossEffectSwapper CreateSwapper(string name, string effectToReplace,
        string[] replacements = null, string attackEffectOptions = null, int minBoost = 0, int maxBoost = 0)
    {
        var swapper = ScriptableHelper.CreateScriptable<FinalBossEffectSwapper>("FinalBossEffectSwapper " + name);

        swapper.effect = AbsentUtils.GetStatus(effectToReplace);
        swapper.replaceWithOptions = replacements?.Select(s => AbsentUtils.GetStatus(s)).ToArray();
        swapper.boostRange = new Vector2Int(minBoost, maxBoost);
        if (attackEffectOptions is not null)
            swapper.replaceWithAttackEffect = AbsentUtils.GetStatus(attackEffectOptions);

        return swapper;
    }
}