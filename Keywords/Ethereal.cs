using System.Reflection;
using AbsentUtilities;
using HarmonyLib;
using UnityEngine;

namespace AbsentAvalanche.Keywords;

public static class Ethereal
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();
    public static readonly string NameWithGuid = $"{AbsentUtils.GetModInfo(Assembly.GetExecutingAssembly()).Mod.GUID}.{Name}";
    public static readonly string Tag = AbstractKeyword.GetTag(Name);
    private static readonly Color Color = new(0.35f, 0.35f, 0.45f);

    public static void Data()
    {
        StatusIconHelper.CreateIconKeyword(NameWithGuid, "Ethereal",
            "Destroy self when <sprite name=ethereal> expires|Counts down every turn while in hand",
            "ethereal",
            Color, new Color(1f, 1f, 1f), Color, new Color(0f, 0f, 0f));
    }
}