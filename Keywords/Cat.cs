using System.Reflection;
using AbsentUtilities;
using UnityEngine;

namespace AbsentAvalanche.Keywords;

public class Cat
{
    public static string Name = "cat";
    public static string NameWithGuid = $"{AbsentUtils.GetModInfo(Assembly.GetExecutingAssembly()).Mod.GUID}.{Name}";
    public static string Tag = AbstractKeyword.GetTag(Name);
    private static readonly Color Color = new(0.35f, 0.35f, 0.45f);
    public static void Data()
    {
        StatusIconHelper.CreateIconKeyword(NameWithGuid, "Cat", "Hit additional times for <1> damage|Watch out for scratch marks",
            "cat",
            Color, new Color(1f, 1f, 1f), Color);
    }
}