using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AbsentAvalanche.Builders.Cards.Companions;
using AbsentAvalanche.Builders.Traits;
using AbsentAvalanche.Helpers;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace AbsentAvalanche.Patches;

[HarmonyPatch]
public static class SelectStartingPetPatches
{
    private static readonly string[] Pets =
    [
        Absent.PrefixGuid(Sally.Name),
        Absent.PrefixGuid(Bam.Name),
        Absent.PrefixGuid(Catcitten.Name),
        Absent.PrefixGuid(LilGuy.Name),
        Absent.PrefixGuid(Chirp.Name),
    ];
    private static readonly List<Entity> OtherPets = [];
    private static bool _altPet;
    private static CardContainer _container;
    private static string _trait = Absent.PrefixGuid(Pilot.Name);

    [UsedImplicitly, HarmonyPostfix, HarmonyPatch(typeof(SelectStartingPet), nameof(SelectStartingPet.Routine))]
    private static IEnumerator Postfix(IEnumerator result, SelectStartingPet __instance, Entity leader)
    {
        if (__instance.leaderSelect.current.entity.traits.Any(t => t.data.name == _trait))
        {
            yield return ChangePets(__instance, leader);
            yield break;
        }
        
        __instance.group.gameObject.SetActive(true);
        _container.gameObject.SetActive(false);
        _altPet = false;
        
        yield return result;
    }

    [UsedImplicitly, HarmonyPostfix, HarmonyPatch(typeof(SelectStartingPet), nameof(SelectStartingPet.SetUp))]
    private static IEnumerator Postfix(IEnumerator result, SelectStartingPet __instance)
    {
        __instance.startPos = __instance.startPos.Add(new Vector3(0, -5, 0));
        
        yield return result;
        
        _container = Object.Instantiate(__instance.group, __instance.transform);
        _container.name = "CustomPetContainer";
        _container.gameObject.SetActive(false);
        
        var clump = new Routine.Clump();
        foreach (var str in Pets)
        {
            clump.Add(CreateAltPet(__instance, str));
        }
        yield return clump.WaitForEnd();
    }
    
    [UsedImplicitly, HarmonyPostfix, HarmonyPatch(typeof(SelectStartingPet), nameof(SelectStartingPet.Cancel))]
    private static void CancelPostfix(SelectStartingPet __instance)
    {
        _container.gameObject.SetActive(false);
        foreach (var entity in OtherPets.OrderBy(_ => Random.Range(0.0f, 1f)))
        {
            LeanTween.moveLocal(entity.gameObject, __instance.startPos, 0.1f).setEaseInQuad().setDelay(Random.Range(0.0f, 0.1f));
            entity.display.hover.SetHoverable(false);
        }
    }
    
    [UsedImplicitly, HarmonyPostfix, HarmonyPatch(typeof(SelectStartingPet), nameof(SelectStartingPet.Select))]
    private static void SelectPostfix(SelectStartingPet __instance, Entity entity)
    {
        if (!__instance.running)
            return;
        var num = OtherPets.IndexOf(entity);
        if (num < 0)
            return;
        __instance.selectedPetIndex = num;
        __instance.selectionSequence.SetUnit(entity);
        __instance.selectionSequence.Begin();
        __instance.cardController.enabled = false;
        __instance.cardController.UnHover();
    }

    [UsedImplicitly, HarmonyPrefix, HarmonyPatch(typeof(SelectStartingPet), nameof(SelectStartingPet.Gain))]
    private static bool GainPrefix(SelectStartingPet __instance, PlayerData playerData)
    {
        LogHelper.Log("Prefix gain");
        if (!_altPet)
        {
            LogHelper.Log("Normal pet");
            return true;
        }
        
        LogHelper.Log("adding Sally");
        var pet = OtherPets[__instance.selectedPetIndex];
        Events.InvokeEntityChosen(pet);
        playerData.inventory.deck.Insert(0, pet.data);
        foreach (var containerEntity in _container.entities)
        {
            CardManager.ReturnToPool(containerEntity);
        }

        _container.gameObject.Destroy();
        return false;
    }
    
    private static IEnumerator CreateAltPet(SelectStartingPet instance, string cardDataName)
    {
        var card = CardManager.Get(AddressableLoader.Get<CardData>("CardData", cardDataName).Clone(), instance.cardController, null, false, true);
        OtherPets.Add(card.entity);
        _container.Insert(0, card.entity);
        LogHelper.Log("Added card " + card.entity.data.title);
        card.transform.localScale = _container.GetChildScale(card.entity);
        card.transform.localPosition = instance.startPos;
        card.hover.SetHoverable(false);
        yield return card.UpdateData();
    }
    
    private static IEnumerator ChangePets(SelectStartingPet instance, Entity leader)
    {
        if (instance.running)
        {
            yield break;
        }
        _altPet = true;
        _container.gameObject.SetActive(true);
        instance.group.gameObject.SetActive(false);
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
            _container.TweenChildPosition(child);
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