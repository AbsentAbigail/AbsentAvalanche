using AbsentAvalanche.StatusEffects;
using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Traits;

internal class GoldRush() : AbstractTrait(Name, Keywords.GoldRush.Name, GoldRushEffect.Name)
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name;
}