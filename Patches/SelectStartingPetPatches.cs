using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AbsentAvalanche.Builders.Cards.Companions;
using AbsentAvalanche.Helpers;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(SelectStartingPet), nameof(SelectStartingPet.Routine))]
public static class SelectStartingPetPatches
{
    public static readonly List<Entity> OtherPets = [];
    public static bool altPet = false;

    [UsedImplicitly]
    private static IEnumerator Postfix(IEnumerator __result, SelectStartingPet __instance, Entity leader)
    {
        if (leader._data.name == Absent.PrefixGuid(Sally.Name + "Leader"))
        {
            __instance.group.Clear();
            yield return ChangePets(__instance, leader);
            yield break;
        }

        altPet = false;
        
        if (__instance.group.ContainsAll(OtherPets))
        {
            __instance.group.Clear();
            var pets = __instance.pets;
            pets.Reverse();
            foreach (var instancePet in pets)
            {
                __instance.group.Add(instancePet);
            }
        }
        
        yield return __result;
    }

    private static IEnumerator ChangePets(SelectStartingPet instance, Entity leader)
    {
        if (instance.running)
        {
            yield break;
        }
        altPet = true;
        instance.running = true;
        instance.leader = leader;
        instance.titleSetter.Set();
        instance.winStreakDisplay.SetActive(false);
        instance.leaderPreContainer = leader.actualContainers[0];
        instance.leaderPreContainerIndex = instance.leaderPreContainer.IndexOf(leader);
        leader.RemoveFromContainers();
        leader.wobbler.WobbleRandom();
        instance.leaderContainer.Add(leader);
        instance.leaderContainer.TweenChildPositions();
        instance.leaderSelect.Hide();
        foreach (var child in OtherPets.OrderBy(_ => Random.Range(0.0f, 1f)))
        {
            instance.group.Add(child);
            instance.group.TweenChildPosition(child);
            child.display.hover.SetHoverable(true);
            yield return new WaitForSeconds(Random.Range(0.0f, 0.1f));
        }
        if ((bool) (Object) instance.text)
        {
            instance.text.SetActive(true);
            instance.text.transform.localScale = Vector3.zero;
            LeanTween.scale(instance.text, Vector3.one, 1f).setEaseOutElastic();
        }
        yield return new WaitUntil(() => !instance.running);
    }
    
}


[HarmonyPatch(typeof(SelectStartingPet), nameof(SelectStartingPet.SetUp))]
public static class SelectStartingPetSetUpPatches
{
    private static IEnumerator Postfix(IEnumerator __result, SelectStartingPet __instance)
    {
        yield return __result;
        
        var petData = new[]
        {
            Absent.PrefixGuid(Sally.Name)
        };
        
        var clump = new Routine.Clump();
        foreach (var str in petData)
        {
            clump.Add(CreateAltPet(__instance, str));
        }
        yield return clump.WaitForEnd();
    }
    
    private static IEnumerator CreateAltPet(SelectStartingPet instance, string cardDataName)
    {
        var card = CardManager.Get(AddressableLoader.Get<CardData>("CardData", cardDataName).Clone(), instance.cardController, null, false, true);
        SelectStartingPetPatches.OtherPets.Add(card.entity);
        LogHelper.Log("Added card " + card.entity.data.title);
        card.transform.localScale = instance.group.GetChildScale(card.entity);
        card.transform.localPosition = instance.startPos;
        card.hover.SetHoverable(false);
        yield return card.UpdateData();
    }
}


[HarmonyPatch(typeof(SelectStartingPet), nameof(SelectStartingPet.Cancel))]
public static class SelectStartingPetCancelPatches
{
    private static void Postfix(SelectStartingPet __instance)
    {
        foreach (var entity in SelectStartingPetPatches.OtherPets.OrderBy(_ => Random.Range(0.0f, 1f)))
        {
            LeanTween.moveLocal(entity.gameObject, __instance.startPos, 0.1f).setEaseInQuad().setDelay(Random.Range(0.0f, 0.1f));
            entity.display.hover.SetHoverable(false);
        }
    }
}

[HarmonyPatch(typeof(SelectStartingPet), nameof(SelectStartingPet.Select))]
public static class SelectStartingPetSelectPatches
{
    private static void Postfix(SelectStartingPet __instance, Entity entity)
    {
        if (!__instance.running)
            return;
        var num = SelectStartingPetPatches.OtherPets.IndexOf(entity);
        if (num < 0)
            return;
        __instance.selectedPetIndex = num;
        __instance.selectionSequence.SetUnit(entity);
        __instance.selectionSequence.Begin();
        __instance.cardController.enabled = false;
        __instance.cardController.UnHover();
    }
}

[HarmonyPatch(typeof(SelectStartingPet), nameof(SelectStartingPet.Gain))]
public static class SelectStartingPetGainPatches
{
    private static bool Prefix(SelectStartingPet __instance, PlayerData playerData)
    {
        LogHelper.Log("Prefix gain");
        if (!SelectStartingPetPatches.altPet)
        {
            SelectStartingPetPatches.OtherPets.Do(pet => pet.Destroy());
            LogHelper.Log("Normal pet");
            return true;
        }
        
        LogHelper.Log("adding Sally");
        var pet = SelectStartingPetPatches.OtherPets[__instance.selectedPetIndex];
        Events.InvokeEntityChosen(pet);
        playerData.inventory.deck.Insert(0, pet.data);
        return false;
    }
}