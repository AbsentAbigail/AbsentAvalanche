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
        if (__result.customData == null || !__result.customData.ContainsKey("Sarcophagus")) return;
        var card = __result.customData.Get<CardData>("Sarcophagus");
        var summon = AbsentUtils.GetStatusOf<StatusEffectSummon>(SummonSarcophagus.Name).InstantiateKeepName();
        summon.summonCard = card;

        var instantSummon = AbsentUtils.GetStatusOf<StatusEffectInstantSummon>(InstantSummonSarcophagus.Name)
            .InstantiateKeepName();
        instantSummon.targetSummon = summon;

        var applyX = AbsentUtils.GetStatusOf<StatusEffectApplyX>(WhenDestroyedSummonSarcophagus.Name)
            .InstantiateKeepName();
        applyX.effectToApply = instantSummon;
        applyX.textInsert = $"<{card.title}>";

        __result.startWithEffects =
        [
            .. __result.startWithEffects,
            new CardData.StatusEffectStacks(applyX, 2)
        ];
    }
}