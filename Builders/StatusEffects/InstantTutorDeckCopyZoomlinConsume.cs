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
public class InstantTutorDeckCopyZoomlinConsume : IStatusBuilder
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
                status.summonCopy = Absent.GetStatusOf<StatusEffectInstantSummon>(InstantSummonDummyToHand.Name);
                status.addEffectStacks =
                [
                    Absent.SStack(TemporarySafeZoomlin.Name),
                    Absent.SStack(TemporarySafeConsume.Name)
                ];
                status.title = LocalizationHelper.GetCollection("UI Text", SystemLanguage.English).GetString(Name);
            });
    }
}