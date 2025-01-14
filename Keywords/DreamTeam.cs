using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Keywords;

public class DreamTeam()
    : AbstractKeyword(Name, "Dream Team",
        "Separate when deployed\nShares charms and upgrades if possible|Has double charm slots")
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();
    public static readonly string Tag = GetTag(Name);
}