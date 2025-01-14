using System.Reflection;
using AbsentUtilities;
using HarmonyLib;
using UnityEngine;

namespace AbsentAvalanche.Keywords;

public class Calm
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public static readonly string NameWithGuid =
        $"{AbsentUtils.GetModInfo(Assembly.GetExecutingAssembly()).Mod.GUID}.{Name}";

    public static readonly string Tag = AbstractKeyword.GetTag(Name);
    private static readonly Color Blue = AbstractKeyword.Color(91, 206, 250);
    private static readonly Color Pink = AbstractKeyword.Color(245, 169, 184);
    private static readonly Color White = AbstractKeyword.Color(255, 255, 255);

    public static void Data()
    {
        StatusIconHelper.CreateIconKeyword(NameWithGuid, "Calm",
            "Reduce max <keyword=counter> for every three <sprite name=calm>|Halves when damage is taken",
            "calm",
            Blue, Pink, White, new Color(0f, 0f, 0f));
    }
}