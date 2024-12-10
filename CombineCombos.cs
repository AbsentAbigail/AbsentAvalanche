using AbsentAvalanche.Cards.Clunkers;
using AbsentAvalanche.Cards.Companion;
using AbsentAvalanche.Cards.Items;
using AbsentAvalanche.Cards.Leaders;
using AbsentUtilities;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AbsentAvalanche;

public static class CombineCombos
{
    private static readonly CombineCardSystem.Combo[] Combos =
    [
        Combo(
            AbsentUtils.PrefixGuid(Bam.Name), AbsentUtils.PrefixGuid(Boozle.Name),
            AbsentUtils.PrefixGuid(Bamboozle.Name)
        ),
        Combo(
            AbsentUtils.PrefixGuid(SalvoKitty.Name), AbsentUtils.PrefixGuid(Catbom.Name),
            AbsentUtils.PrefixGuid(FusilladeCat.Name)
        ),
        Combo(
            AbsentUtils.PrefixGuid(Catcus.Name), AbsentUtils.PrefixGuid(Catcitten.Name),
            AbsentUtils.PrefixGuid(Catci.Name)
        ),
        .. LeaderCombo(Bubbles.Name, Cuddles.Name, BubblesAndCuddles.Name),
    ];

    public static void SceneLoaded(Scene scene)
    {
        if (scene.name != "Campaign")
            return;

        var combineCardSystem = Object.FindObjectOfType<CombineCardSystem>(true);
        combineCardSystem.enabled = true;
        combineCardSystem.combos = combineCardSystem.combos.AddRangeToArray(Combos);
    }

    private static CombineCardSystem.Combo[] LeaderCombo(string card1, string card2, string resultingCardName)
    {
        var companion1 = AbsentUtils.PrefixGuid(card1);
        var leader1 = AbsentUtils.PrefixGuid(card1 + Leader<Sam>.Suffix);
        var companion2 = AbsentUtils.PrefixGuid(card2);
        var leader2 = AbsentUtils.PrefixGuid(card2 + Leader<Sam>.Suffix);
        var resultingCompanion = AbsentUtils.PrefixGuid(resultingCardName);
        var resultingLeader = resultingCompanion + Leader<Sam>.Suffix;

        return
        [
            Combo(companion1, companion2, resultingCompanion),
            Combo(leader1, companion2, resultingLeader),
            Combo(companion1, leader2, resultingLeader)
        ];
    }

    private static CombineCardSystem.Combo Combo(string card1, string card2, string result)
    {
        return new CombineCardSystem.Combo
        {
            cardNames = [card1, card2],
            resultingCardName = result
        };
    }
}