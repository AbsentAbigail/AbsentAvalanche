using System.Linq;
using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using UnityEngine;

namespace AbsentAvalanche.StatusEffects;

public class InstantTutorThreeRandomCompanions() : AbstractStatus<StatusEffectInstantTutor>(Name, subscribe: status =>
{
    status.source = StatusEffectInstantTutor.CardSource.Custom;
    status.summonCopy = AbsentUtils.GetStatusOf<StatusEffectInstantSummon>(InstantSummonDummyToHand.Name);
    status.amount = 3;
    status.Predicate = cardData => cardData.cardType.name == "Friendly" && !cardData.IsPet();
    status.title = LocalizationHelper.GetCollection("UI Text", SystemLanguage.English).GetString(Name);
})
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}