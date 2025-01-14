using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Traits;

public class Trample() : AbstractTrait(Name, Keywords.Trample.Name, OnKillTriggerNoTrigger.Name)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}