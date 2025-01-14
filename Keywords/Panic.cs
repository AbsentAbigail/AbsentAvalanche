using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Keywords;

public class Panic() : AbstractKeyword(Name, "Panic", "When deployed, gain for each enemy")
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();
    public static string Tag = GetTag(Name);
}