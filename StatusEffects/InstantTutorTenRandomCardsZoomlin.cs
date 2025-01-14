using System.Linq;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using UnityEngine;

namespace AbsentAvalanche.StatusEffects;

public class InstantTutorTenRandomCardsZoomlin() : AbstractStatus<StatusEffectInstantTutor>(Name, subscribe: status =>
{
    status.source = StatusEffectInstantTutor.CardSource.Custom;
    status.summonCopy = AbsentUtils.GetStatusOf<StatusEffectInstantSummon>(InstantSummonDummyToHand.Name);
    status.amount = 10;

    status.Predicate = cardData => 
        cardData.cardType.name == "Item" &&
        cardData.playType != Card.PlayType.None &&
        (cardData.traits is null || !cardData.traits.Exists(b => b.data.name is "Recycle"));
    
    status.addEffectStacks = [AbsentUtils.SStack(TemporarySafeZoomlin.Name)];
    status.title = LocalizationHelper.GetCollection("UI Text", SystemLanguage.English).GetString(Name);
})
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}