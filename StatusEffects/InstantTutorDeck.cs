using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using UnityEngine;

namespace AbsentAvalanche.StatusEffects;

public class InstantTutorDeck() : AbstractStatus<StatusEffectInstantTutor>(Name,
    subscribe: status =>
        status.title = LocalizationHelper.GetCollection("UI Text", SystemLanguage.English).GetString(Name))
{
    public const string Name = "InstantTutorDeck";
}