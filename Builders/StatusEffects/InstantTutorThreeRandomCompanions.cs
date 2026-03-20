#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class InstantTutorThreeRandomCompanions : IStatusBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;

    public DataFileBuilder<StatusEffectData, StatusEffectDataBuilder> Builder()
    {
        return new StatusEffectDataBuilder(Absent.Instance)
            .Create<StatusEffectInstantTutor>(Name)
            .WithStackable(true)
            .WithCanBeBoosted(false)
            .SubscribeToAfterAllBuildEvent<StatusEffectInstantTutor>(status =>
            {
                status.source = StatusEffectInstantTutor.CardSource.Custom;
                status.summonCopy = Absent.GetStatusOf<StatusEffectInstantSummon>(InstantSummonDummyToHand.Name);
                status.amount = 3;
                status.predicate = cardData => cardData.cardType.name == "Friendly" && !cardData.IsPet();
                status.title = LocalizationHelper.GetCollection("UI Text", SystemLanguage.English).GetString(Name);
            });
    }
}