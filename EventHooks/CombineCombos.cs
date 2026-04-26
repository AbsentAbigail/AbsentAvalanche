#region

using AbsentAvalanche.Builders.Cards.Clunkers;
using AbsentAvalanche.Builders.Cards.Companions;
using AbsentAvalanche.Builders.Cards.Items;
using AbsentAvalanche.GameSystems;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace AbsentAvalanche.EventHooks;

public static class CombineCombos
{
    private static readonly CombineCardSystem.Combo[] Combos =
    [
        Combo(
            Absent.PrefixGuid(Bam.Name), Absent.PrefixGuid(Boozle.Name),
            Absent.PrefixGuid(Bamboozle.Name)
        ),
        Combo(
            Absent.PrefixGuid(SalvoKitty.Name), Absent.PrefixGuid(Catbom.Name),
            Absent.PrefixGuid(FusilladeCat.Name)
        ),
        Combo(
            Absent.PrefixGuid(Catcus.Name), Absent.PrefixGuid(Catcitten.Name),
            Absent.PrefixGuid(Catci.Name)
        ),
        .. LeaderCombo(Bubbles.Name, Cuddles.Name, BubblesAndCuddles.Name),
        .. LeaderCombo(Sherba.Name, Cuddles.Name, SherbaAndCuddles.Name),
        .. LeaderCombo(April.Name, May.Name, AprilAndMay.Name),
        .. LeaderCombo(Alice.Name, Nami.Name, AliceAndNami.Name),
        .. LeaderCombo(Bubbles.Name, Kiki.Name, BubblesAndKiki.Name),
    ];

    public static void SceneLoaded(Scene scene)
    {
        if (scene.name != "Campaign")
            return;

        var combineCardSystem = Object.FindObjectOfType<CombineCardSystem>(true);
        combineCardSystem.enabled = true;
        combineCardSystem.combos = combineCardSystem.combos.AddRangeToArray(Combos);

        GameObject.Find("Systems")?.AddComponent<ChargeRedrawBellSystem>();
    }

    private static CombineCardSystem.Combo[] LeaderCombo(string card1, string card2, string resultingCardName)
    {
        var companion1 = Absent.PrefixGuid(card1);
        var leader1 = Absent.PrefixGuid(card1 + "Leader");
        var companion2 = Absent.PrefixGuid(card2);
        var leader2 = Absent.PrefixGuid(card2 + "Leader");
        var resultingCompanion = Absent.PrefixGuid(resultingCardName);

        return
        [
            Combo(companion1, companion2, resultingCompanion),
            Combo(leader1, companion2, resultingCompanion),
            Combo(companion1, leader2, resultingCompanion)
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