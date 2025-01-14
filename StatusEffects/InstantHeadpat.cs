using AbsentAvalanche.StatusEffects.Implementations;
using AbsentUtilities;
using HarmonyLib;
using UnityEngine;

namespace AbsentAvalanche.StatusEffects;

public class InstantHeadpat() : AbstractStatus<StatusEffectInstantPlayGif>(Name, canStack: false, subscribe: status =>
{
    status.animationKey = "PetTheBlank";
    status.positionOffset = new Vector3(0, 0.8f);
})
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}