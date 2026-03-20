#region

using AbsentAvalanche.Builders.Cards.Items;
using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.StatusEffectImplementations;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

#endregion

namespace AbsentAvalanche.Builders.StatusEffects;

[UsedImplicitly]
public class InstantTutorTenRandomCardsZoomlin : IStatusBuilder
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
                status.amount = 10;

                status.predicate = cardData =>
                    cardData.cardType.name == "Item" &&
                    cardData.playType != Card.PlayType.None &&
                    (cardData.traits is null || !cardData.traits.Exists(b => b.data.name is "Recycle")) &&
                    cardData.name != Absent.PrefixGuid(Sarcophagus.Name);

                status.addEffectStacks = [Absent.SStack(TemporarySafeZoomlin.Name)];
                status.title = LocalizationHelper.GetCollection("UI Text", SystemLanguage.English).GetString(Name);
            });
    }
}