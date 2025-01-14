using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;

namespace AbsentAvalanche.Keywords;

public class Royal()
    : AbstractKeyword(Name, "Royal", "Counts as an additional <Leader>|Lose when all leaders die")
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();
    public static string Tag = GetTag(Name);
}