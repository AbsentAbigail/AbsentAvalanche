using System.Reflection;
using AbsentUtilities;
using HarmonyLib;
using UnityEngine;

namespace AbsentAvalanche.Keywords;

public class Abduct
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public static readonly string NameWithGuid =
        $"{AbsentUtils.GetModInfo(Assembly.GetExecutingAssembly()).Mod.GUID}.{Name}";

    public static readonly string Tag = AbstractKeyword.GetTag(Name);
    private static readonly Color Blue = AbstractKeyword.Color(74, 57, 148);
    private static readonly Color Pink = AbstractKeyword.Color(100, 67, 158);
    private static readonly Color White = AbstractKeyword.Color(255, 255, 255);

    public static void Data()
    {
        StatusIconHelper.CreateIconKeyword(NameWithGuid, "Abduct",
            "Untargetable and <keyword=snow>'d for one turn",
            "abduct",
            Blue, Pink, White, new Color(0f, 0f, 0f));
    }
}