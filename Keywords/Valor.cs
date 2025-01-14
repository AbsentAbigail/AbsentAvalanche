using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Keywords;

public class Valor()
    : AbstractKeyword(Name, "Valour", "Hits target in row with highest <keyword=attack>|Front-most unit breaks ties")
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();
    public static string Tag = GetTag(Name);
}