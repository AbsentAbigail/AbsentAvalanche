using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Traits;

public class Scavenge() : AbstractTrait(Name, Keywords.Scavenge.Name, OnBossKillGainRandomCharm.Name)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}