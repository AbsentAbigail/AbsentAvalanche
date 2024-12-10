using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.Keywords;

public class PowerDuo()
    : AbstractKeyword(Name, "Dream Team", "Separate when deployed\nShares charms and upgrades if possible|Has double charm slots")
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();
    public static string Tag = GetTag(Name);
}