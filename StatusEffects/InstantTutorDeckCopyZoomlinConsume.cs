using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using UnityEngine;

namespace AbsentAvalanche.StatusEffects;

public class InstantTutorDeckCopyZoomlinConsume() : AbstractStatus<StatusEffectInstantTutor>(Name, subscribe: status =>
{
    status.summonCopy = AbsentUtils.GetStatusOf<StatusEffectInstantSummon>(InstantSummonDummyToHand.Name);
    status.addEffectStacks = [
        AbsentUtils.SStack(TemporarySafeZoomlin.Name),
        AbsentUtils.SStack(TemporarySafeConsume.Name),
    ];
    status.title = LocalizationHelper.GetCollection("UI Text", SystemLanguage.English).GetString(Name);
})
{
    public const string Name = "InstantTutorDeckCopyZoomlinConsume";
}