#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.Scriptables.CardImage;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace AbsentAvalanche.Builders.Cards.Companions;

[UsedImplicitly]
public class Val : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Val",
                "TargetModeBasic",
                "Blood Profile Blue (x2)",
                "FloatAnimationProfile")
            .SetStats(4, 4, 5)
            .SetSprites(
                Absent.GetSprite("Val"),
                Absent.GetSprite("ValBG"))
            .WithFlavour("Has a zipper for a mouth!")
            .WithPools(CardPools.GeneralUnits)
            .DropsBling(4)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects = [Absent.SStack(OnHitEat.Name)];
                card.greetMessages =
                [
                    "Has a zipper for a mouth!",
                    "In the mouth of this big blue whale there is room for pajamas or a treasure. It’s because this soft animal is a true friend who can keep a secret, play and give hugs when needed."
                ];

                card.scriptableImagePrefab = CreateScriptableCardImage<ValCardImage>("val");
            });
    }
    
    // Code by Phan
    private static T CreateScriptableCardImage<T>(string name) where T : ScriptableCardImage
    {
        // Create a new GameObject that will host the ScriptableImage
        var ghostObject = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(T))
        {
            // HideAndDontSave so it doesn't get touched during gameplay, OR
            hideFlags = HideFlags.HideAndDontSave
        };

        // ensure the GameObject is kept in memory this session
        Object.DontDestroyOnLoad(ghostObject);

        // Set the GameObject's size to the card size
        ghostObject.GetComponent<RectTransform>().sizeDelta = new Vector2(3.8f, 5.7f);

        // The image will try to autofill to fit the RectTransform size
        ghostObject.GetComponent<Image>().preserveAspect = true;
        // This fixes the card being hoverable
        ghostObject.GetComponent<Image>().raycastTarget = false;

        return ghostObject.GetComponent<T>();
    }
}