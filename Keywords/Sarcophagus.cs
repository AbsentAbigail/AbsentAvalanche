using AbsentUtilities;
using HarmonyLib;

namespace AbsentAvalanche.Keywords;

public class Sarcophagus()
    : AbstractKeyword(Name, "Sarcophagus", $"3{Ethereal.Tag}\n" +
                                           $"Add <2> <of the sealed card> to hand when destroyed|Sealed card keeps charms")
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();
    public static string Tag = GetTag(Name);
}