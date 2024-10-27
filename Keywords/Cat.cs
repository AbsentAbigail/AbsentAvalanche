using System.Reflection;
using AbsentUtilities;
using UnityEngine;

namespace AbsentAvalanche.Keywords;

public static class Cat
{
    public const string Name = "cat";
    public static readonly string NameWithGuid = $"{AbsentUtils.GetModInfo(Assembly.GetExecutingAssembly()).Mod.GUID}.{Name}";
    public static readonly string Tag = AbstractKeyword.GetTag(Name);
    private static readonly Color Color = new(0.2f, 0.2f, 0.4f);

    public static void Data()
    {
        StatusIconHelper.CreateIconKeyword(NameWithGuid, "Cat",
            "Hit additional times for <1> damage|Watch out for the claws",
            "cat",
            Color, new Color(1f, 1f, 1f), Color);
    }
}