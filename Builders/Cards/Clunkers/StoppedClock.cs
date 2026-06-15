using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.StatusEffects;
using AbsentAvalanche.Helpers;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace AbsentAvalanche.Builders.Cards.Clunkers;

[UsedImplicitly]
public class StoppedClock : ICardBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<CardData, CardDataBuilder> Builder()
    {
        return new CardDataBuilder(Absent.Instance)
            .CreateUnit(Name, "Stopped Clock", idleAnim: "SwayAnimationProfile")
            .WithCardType("Clunker")
            .SetHealth(null)
            .SetDamage(null)
            .SetSprites(
                Absent.GetSprite("StoppedClock"),
                Absent.GetSprite("StoppedClockBG"))
            .WithPools(CardPools.GeneralItems)
            .SubscribeToAfterAllBuildEvent(card =>
            {
                card.startWithEffects =
                [
                    Absent.SStack("Scrap"),
                    Absent.SStack(FreePlay.Name, 3)
                ];
                card.scriptableImagePrefab = Absent.CreateScriptableCardImage<StoppedClockCardImage>("stopped_clock");
            });
    }
}

internal class StoppedClockCardImage : ScriptableCardImage
{
    public Image Image => GetComponent<Image>();

    public Sprite[] sprites =
    [
        Absent.GetSprite("StoppedClock"),
        Absent.GetSprite("StoppedClock1"),
        Absent.GetSprite("StoppedClock2"),
    ];

    private int _timer;
    private int _counter;
    private bool _freePlay = true;

    public override void AssignEvent()
    {
        Image.sprite = entity.data.mainSprite;
    }

    public void Update()
    {
        if (_freePlay)
        {
            return;
        }
        
        _timer++;
        if (_timer <= 20)
        {
            return;
        }

        _timer = 0;
        _counter = (_counter + 1) % 6;
        Image.sprite = _counter switch
        {
            0 or 1 => sprites[0],
            2 or 5 => sprites[1],
            3 or 4 => sprites[2],
            _ => Image.sprite
        };
    }

    public override void UpdateEvent()
    {
        var freePlay = entity.FindStatus<StatusEffectFreePlay>("freeplay");
        _freePlay = freePlay;

    }
}